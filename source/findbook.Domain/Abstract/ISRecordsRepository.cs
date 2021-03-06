﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using findbook.Domain.Entities;

namespace findbook.Domain.Abstract {
    public interface ISRecordsRepository {
        IQueryable<SRecords> SRecords { get; }

        void Collect(string userID, string sBody);
    }
}
