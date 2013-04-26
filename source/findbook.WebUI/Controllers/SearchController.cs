using System.Web.Mvc;
using findbook.Domain.Abstract;
using PanGu;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using System.Data.SqlClient;
using Lucene.Net.Analysis.PanGu;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Store;
using Lucene.Net.Search;
using findbook.Domain.Entities;
using System.Linq;
using System;
using findbook.WebUI.Models;
using System.Drawing;
using System.Web;

namespace findbook.WebUI.Controllers {
    public class SearchController : Controller {
        private IUsersRepository ur;
        private ISRecordsRepository rr;
        private IBooksRepository br;

        public SearchController(IUsersRepository userRepository, ISRecordsRepository searcordRepository,
                                IBooksRepository bookRepository) {
            ur = userRepository;
            rr = searcordRepository;
            br = bookRepository;
        }

        [HttpPost]
        public ActionResult Show(string kw = "", string orderType = "0") {
            if (string.IsNullOrEmpty(kw)) {
                kw = HttpContext.Request["kw"].ToString(); 
            }
            
            ViewData["searchWord"] = kw;
            SearchView sv = Search(kw);

            return View(sv);
        }

        //搜索
        public SearchView Search(string kw) {
            //如果用户登陆则收集用户搜索输入
            if (HttpContext.User.Identity.IsAuthenticated) {
                //从cookie中获取userID
                HttpCookie cookie = Request.Cookies["user"];
                string userID = cookie["userID"].ToString();

                //string userID = (string)Session["userID"];
                rr.Collect(userID, kw);
            }

            #region 获取List<Book>
            ////根据相对路径获取绝对路径
            //string path = HttpContext.Server.MapPath("../Index");

            ////初始化参数
            //FSDirectory directory = FSDirectory.Open(new DirectoryInfo(path), new NoLockFactory());
            //IndexReader reader = IndexReader.Open(directory, true);
            //IndexSearcher searcher = new IndexSearcher(reader);
            //PhraseQuery query = new PhraseQuery();

            ////分词
            //List<string> sw = Splitwords(kw);
            
            //foreach (string word in sw) {
            //    query.Add(new Term("bookName", word));
            //    query.Add(new Term("upUserNickName", word));
            //    query.Add(new Term("bookIntr", word));
            //    query.Add(new Term("author", word));
            //    query.Add(new Term("pub", word));
            //}

            ////相聚100以内才算是查询到
            //query.SetSlop(1000);
            //TopScoreDocCollector collector = TopScoreDocCollector.create(1024, true);//最大1024条记录
            //searcher.Search(query, null, collector);

            ////搜索到的信息
            //int totalCount = collector.GetTotalHits();
            //int startRowIndex = 0;
            //int pageSize = 10;

            ////分页,下标应该从0开始吧，0是第一条记录
            //ScoreDoc[] docs = collector.TopDocs(startRowIndex, pageSize).scoreDocs;
            
            ////将搜索到的信息，装入数组中
            //List<Books> bookResult = new List<Books>();

            ////将搜索结果中的对象赋值到List<Books>中
            //for (int i = 0; i < docs.Length; i++) {
            //    //取文档的编号，这个是主键，lucene.net分配
            //    //检索结果中只有文档的id，如果要取Document，则需要Doc再去取
            //    int docID = docs[i].doc;
            //    Document doc = searcher.Doc(docID);

            //    string bookID = doc.Get("bookID");
            //    string bookName = doc.Get("bookName");
            //    string upTime = doc.Get("upTime");
            //    string upUserID = doc.Get("upUserID");
            //    string upUserNickName = doc.Get("upUserNickName");
            //    string recNumber = doc.Get("recNumber");
            //    string remNumber = doc.Get("remNumber");
            //    string bookIntr = doc.Get("bookIntr");
            //    string bookPrice = doc.Get("bookPrice");
            //    string author = doc.Get("author");
            //    string pub = doc.Get("pub");
            //    string cNumber = doc.Get("cNumber");

            //    //高亮搜索结果
            //    foreach (string word in sw) { 
            //        bookName = Preview(bookName, word);
            //        upUserNickName = Preview(upUserNickName, word);
            //        bookIntr = Preview(bookIntr, word);
            //        author = Preview(author, word);
            //        pub = Preview(pub, word);
            //    }

            //    //将搜索结果放入Book对象中
            //    Books bk = new Books();
            //    bk.author = author;
            //    bk.bookID = bookID;
            //    bk.bookIntr = bookIntr;
            //    bk.bookName = bookName;
            //    bk.bookPrice = Decimal.Parse(bookPrice);
            //    bk.cNumber = Int32.Parse(cNumber);
            //    bk.pub = pub;
            //    bk.recNumber = Int32.Parse(recNumber);
            //    bk.remNumber = Int32.Parse(remNumber);
            //    bk.upTime = DateTime.Parse(upTime);
            //    bk.upUserNickName = upUserNickName;
            //    bk.upUserID = upUserID;

            //    bookResult.Add(bk);
            //}

            

            #endregion

            //获取匹配的用户
            SearchView sv = new SearchView {
                Books = br.Books.Where(b => b.bookName.Contains(kw) || b.author.Contains(kw) 
                                || b.bookZY.Contains(kw) || b.bookXY.Contains(kw)),

                Users = ur.Users.Where(u => u.userName.Contains(kw))
            };



            return sv; 
        }

        #region 高亮
        private string Preview(string body, string keyword) {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"Red\">", "</font>");
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 1000;
            string bodyPreview = highlighter.GetBestFragment(keyword, body);
            return bodyPreview;
        }
        #endregion

        #region 分词
        public List<string> Splitwords(string word) {
            List<string> words = new List<string>();
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(word));
            Lucene.Net.Analysis.Token token = null;
            while ((token = tokenStream.Next()) != null) {
                words.Add(token.TermText());
            }
            return words;
        }
        #endregion

        #region 创建索引
        public SqlDataReader OpenTable()  //创建索引1
        {
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();
                using (SqlCommand mycom = new SqlCommand("select * from Books", mycon)) {
                    return mycom.ExecuteReader();
                }
            }
        }

        public IndexWriter CreateIndex(SqlDataReader myred) //创建索引2
        {           
            //根据相对路径获取绝对路径
            string path = HttpContext.Server.MapPath("../Index");

            IndexWriter writer = new IndexWriter(path, new PanGuAnalyzer(), true);
            {
                //建立索引字段
                while (myred.Read()) {
                    //向Document中添加字段
                    Document doc = new Document();
                    doc.Add(new Field("bookID", myred["bookID"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("bookName", myred["bookName"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("upTime", myred["upTime"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("upUserID", myred["upUserID"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("upUserNickName", myred["upUserNiceName"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("recNumber", myred["recNumber"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("remNumber", myred["remNumber"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("bookIntr", myred["bookIntr"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("bookPrice", myred["bookPrice"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("author", myred["author"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    doc.Add(new Field("pub", myred["pub"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("cNumber", myred["cNumber"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    
                    writer.AddDocument(doc);
                }
                writer.Optimize();
                writer.Close();
            }

            return writer;
        }
#endregion
    }

}

