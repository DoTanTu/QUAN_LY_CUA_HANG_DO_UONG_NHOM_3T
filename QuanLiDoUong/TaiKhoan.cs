using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuanLiDoUong
{
    public partial class TaiKhoan : Form
    {
        public TaiKhoan()
        {
            InitializeComponent();
        }

        public static string path = "../../User.xml";

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(path);
            DataTable dt = new DataTable();
            dt = dataSet.Tables["User"];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                listView1.Items.Add(dr["ID"].ToString());
                listView1.Items[i].SubItems.Add(dr["hoTen"].ToString());
                listView1.Items[i].SubItems.Add(dr["tenDangNhap"].ToString());
                listView1.Items[i].SubItems.Add(dr["matKhau"].ToString());
                i++;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string day = DateTime.Now.Day.ToString();
            string mouth = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();

            long id = long.Parse(day + mouth + year + hour + minute + second);
            try
            {
                XDocument testXML = XDocument.Load(path);
                XElement newUser = new XElement("User",
                    new XElement("hoTen", txtTen.Text),
                    new XElement("tenDangNhap", txtUserName.Text),
                    new XElement("matKhau", txtPass.Text));

                var lastUser = testXML.Descendants("User").Last();
                long newID = Convert.ToInt64(lastUser.Attribute("ID").Value);
                //newUser.SetAttributeValue("ID",);
                testXML.Element("Users").Add(newUser);
                testXML.Save(path);
                TaiKhoan_Load(sender, e);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
