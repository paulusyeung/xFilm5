namespace xFilm5.Settings
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    using Gizmox.WebGUI.Server;
    using Gizmox.WebGUI.Forms;

    using xFilm5.DAL;
    using System.ComponentModel;

    public class Utility
    {
        public static void SetCookies(string key, string value)
        {
            System.Web.HttpCookie oCookie = new System.Web.HttpCookie(key);
            DateTime now = DateTime.Now;

            oCookie.Value = value;
            oCookie.Expires = now.AddYears(1);

            System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
        }

        /// <summary>
        /// Convert the datetime value to string with time or without.
        /// If the value is equaled to 1900-01-01, it would return a emty value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="withTime"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime value, bool withTime)
        {
            string formatString = GetDateFormat();
            if (withTime)
            {
                formatString = GetDateTimeFormat();
            }

            if (!value.Equals(new DateTime(1900, 1, 1)))
            {
                return value.ToString(formatString);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetDateFormat()
        {
            string result = String.Empty;

            switch (VWGContext.Current.CurrentUICulture.ToString())
            {
                case "zh-CHS":
                    result = "yyyy-MM-dd";
                    break;
                case "zh-CHT":
                    result = "dd/MM/yyyy";
                    break;
                case "en-US":
                default:
                    result = "dd/MM/yyyy";
                    break;
            }

            return result;
        }

        public static string GetDateTimeFormat()
        {
            string result = String.Empty;

            switch (VWGContext.Current.CurrentUICulture.ToString())
            {
                case "zh-CHS":
                    result = "yyyy-MM-dd HH:mm";
                    break;
                case "zh-CHT":
                    result = "dd/MM/yyyy HH:mm";
                    break;
                case "en-US":
                default:
                    result = "dd/MM/yyyy HH:mm";
                    break;
            }

            return result;
        }

        public static int GetQtyDecimalPoint()
        {
            int result = 0;

            return result;
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public static byte[] ReadFully(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
    }

    public class ComboBoxDefault
    {
        public static void Priority(ref ComboBox target)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            string defaultValue = "Regular";
            if (ConfigurationSettings.AppSettings["Priority"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["Priority"];
            }
            defaultValue = oDict.GetWord(defaultValue);

            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void CorelDraw(ref ComboBox target)
        {
            string defaultValue = "9.0C";
            if (ConfigurationSettings.AppSettings["CorelDraw"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["CorelDraw"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void Illustrator(ref ComboBox target)
        {
            string defaultValue = "8.0";
            if (ConfigurationSettings.AppSettings["Illustrator"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["Illustrator"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void FreeHand(ref ComboBox target)
        {
            string defaultValue = "8.0C";
            if (ConfigurationSettings.AppSettings["FreeHand"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["FreeHand"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void PageMaker(ref ComboBox target)
        {
            string defaultValue = "6.5C";
            if (ConfigurationSettings.AppSettings["PageMaker"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["PageMaker"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void PhotoShop(ref ComboBox target)
        {
            string defaultValue = "8C CS";
            if (ConfigurationSettings.AppSettings["PhotoShop"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["PhotoShop"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void QuarkXpress(ref ComboBox target)
        {
            string defaultValue = "4.0";
            if (ConfigurationSettings.AppSettings["QuarkXpress"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["QuarkXpress"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void MsWord(ref ComboBox target)
        {
            string defaultValue = "2003 XP";
            if (ConfigurationSettings.AppSettings["MsWord"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["MsWord"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void LSR(ref ComboBox target)
        {
            string defaultValue = "175 LPI / 2400 DPI";
            if (ConfigurationSettings.AppSettings["LSR"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["LSR"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
        public static void StandardSize(ref ComboBox target)
        {
            string defaultValue = "A4";
            if (ConfigurationSettings.AppSettings["StandardSize"] != null)
            {
                defaultValue = ConfigurationSettings.AppSettings["StandardSize"];
            }
            int index = target.FindString(defaultValue, 1);
            if (index >= 0 && index < target.Items.Count)
            {
                target.SelectedIndex = index;
            }
        }
    }

    public class LoadComboBox
    {
        public static void Priority(ref ComboBox target)
        {
            string[] orderBy = { "Name" };
            xFilm5.DAL.T_PriorityCollection items = xFilm5.DAL.T_Priority.LoadCollection(orderBy, true);
            if (items.Count > 0)
            {
                nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

                ComboList ds = new ComboList();
                target.Items.Clear();

                foreach (xFilm5.DAL.T_Priority item in items)
                {
                    ds.Add(new ComboItem(oDict.GetWord(item.Name), item.ID));
                }

                target.DataSource = ds;
                target.DisplayMember = "Code";
                target.ValueMember = "Id";

                target.SelectedIndex = 0;
            }
        }

        public static void ProofingMaterial(ref ComboBox target)
        {
            string[] orderBy = { "Name" };
            xFilm5.DAL.T_ProofingMaterialCollection items = xFilm5.DAL.T_ProofingMaterial.LoadCollection(orderBy, true);
            if (items.Count > 0)
            {
                nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

                ComboList ds = new ComboList();
                target.Items.Clear();

                foreach (xFilm5.DAL.T_ProofingMaterial item in items)
                {
                    ds.Add(new ComboItem(oDict.GetWord(item.Name), item.ID));
                }

                target.DataSource = ds;
                target.DisplayMember = "Code";
                target.ValueMember = "Id";

                target.SelectedIndex = 0;
            }
        }

        public static void DigitalProof(ref ComboBox target)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            target.Items.Clear();

            target.Items.Add(oDict.GetWord("blueprints"));
            target.Items.Add(oDict.GetWord("glossy_paper"));

            target.SelectedIndex = 0;
        }

        public static void GripperEdge(ref ComboBox target)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            target.Items.Clear();

            target.Items.Add(oDict.GetWord("gripper_edge_head"));
            target.Items.Add(oDict.GetWord("gripper_edge_heel"));

            target.SelectedIndex = 0;
        }

        #region ComboBox Binding List

        private class ComboItem
        {
            private string _code = string.Empty;
            private int _id = 0;

            public ComboItem(string code, int id)
            {
                _code = code;
                _id = id;
            }

            public string Code
            {
                get
                {
                    return _code;
                }
                set
                {
                    _code = value;
                }
            }

            public int Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;
                }
            }
        }

        private class ComboList : BindingList<ComboItem>
        {
        }

        #endregion
    }
}
