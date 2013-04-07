using System.Linq;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface ILeaveCommentsRepository {
        IQueryable<LeaveComments> LeaveComments { get; }

        bool LeaveComment(string lUserID, string lUserName, string hUserID, string hUserName, string lBody);
    }


}
