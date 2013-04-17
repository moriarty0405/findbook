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

namespace findbook.WebUI.Controllers {
    public class SearchController : Controller {
        //private ISRecordsRepository rr;
        //private ISKeysRepository kr;

        //public SearchController(ISRecordsRepository recordRepository, ISKeysRepository keyRepository) {
        //    rr = recordRepository;
        //    kr = keyRepository;
        //}

        #region 搜索
        public ActionResult Search(string kw) {
            //根据相对路径获取绝对路径
            string path = HttpContext.Server.MapPath("../Index");

            //初始化参数
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(path), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            PhraseQuery query = new PhraseQuery();

            //分词
            foreach (string word in Splitwords(kw)) {
                query.Add(new Term("bookName", word));
            }

            //相聚100以内才算是查询到
            query.SetSlop(1000);
            TopScoreDocCollector collector = TopScoreDocCollector.create(1024, true);//最大1024条记录
            searcher.Search(query, null, collector);

            //搜索到的信息
            int totalCount = collector.GetTotalHits();
            int startRowIndex = 0;
            int pageSize = 10;

            //分页,下标应该从0开始吧，0是第一条记录
            ScoreDoc[] docs = collector.TopDocs(startRowIndex, pageSize).scoreDocs;
            
            //将搜索到的信息，装入数组中
            List<Books> result = new List<Books>();


            return View(); 
        }
        #endregion

        //高亮
        private string Preview(string body, string keyword) {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"Red\">", "</font>");
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 1000;
            string bodyPreview = highlighter.GetBestFragment(keyword, body);
            return bodyPreview;
        }   

        //分词
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

        #region 创建索引
        public SqlDataReader OpenTable()  //创建索引1
        {
            string connstr = ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            using (SqlConnection mycon = new SqlConnection(connstr)) {
                mycon.Open();
                using (SqlCommand mycom = new SqlCommand("select bookID, bookName, upUserID from Books", mycon)) {
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
                    Document doc = new Document();
                    doc.Add(new Field("bookID", myred["bookID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    doc.Add(new Field("upUserID", myred["upUserID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
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

