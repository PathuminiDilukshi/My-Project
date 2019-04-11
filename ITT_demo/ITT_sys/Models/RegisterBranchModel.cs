﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITT_sys.Models
{
    public class RegisterBranchModel
    {
        public string BankCode
        {
            get;
            set;

        }
        public string BranchCode
        {
            get;
            set;

        }
        public string BranchName
        {
            get;
            set;

        }
        public string AddressLine1
        {
            get;
            set;

        }
        public string AddressLine2
        {
            get;
            set;

        }
        public string AddressLine3
        {
            get;
            set;

        }
        public string ContactNo
        {
            get;
            set;

        }
        public string Email
        {
            get;
            set;

        }

        public string BankName { get; set; }

        public string BranchCode_update { get; set; }
      
    }
}
