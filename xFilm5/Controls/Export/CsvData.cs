using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FileHelpers;

namespace xFilm5.Controls.Export
{
    public class CsvData
    {
        [DelimitedRecord("|")]
        public class ClientEmailCsv
        {
            public string Branch;
            public string ClientId;
            public string ClientName;
            public string Contact;
            public string Email;
        }

        [DelimitedRecord("|")]
        public class ClientDetailsCsv
        {
            public string ClientId;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string ClientName;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Branch;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Address;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Tel;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Fax;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Contact;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Mobile;
            [FieldOptional()]
            [FieldNullValue(typeof(String), "")]
            public string Email;
        }
    }
}
