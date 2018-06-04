using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace zhangping
{
    public partial class Form1 : Form
    {
        public static string Dir;
        public static string cDir;
        public static string rDir;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.label1.Text = "已选择文件夹:" + foldPath;
                Dir = foldPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            try {
                if (!Directory.Exists(Dir+"\\out"))   
                {
                    Directory.CreateDirectory(Dir + "\\out");   
                }
                DirectoryInfo dir = new DirectoryInfo(@"" + Dir + "");
                int i = 0;
                foreach (FileInfo dChild in dir.GetFiles("*.xml"))
                {
                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(dChild.FullName);
                        //xmlDoc.SelectSingleNode("WMSANS/Status").InnerText = "FIN";
                        XmlDocument newXmlDoc = new XmlDocument();
                        newXmlDoc.AppendChild(newXmlDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                        XmlElement xe = newXmlDoc.CreateElement("WMSANS");
                        newXmlDoc.AppendChild(xe);
                        XmlElement MessageSenderIdentifier = newXmlDoc.CreateElement("MessageSenderIdentifier");
                        XmlElement MessageSenderName = newXmlDoc.CreateElement("MessageSenderName");
                        XmlElement MessageReceiverIdentifier = newXmlDoc.CreateElement("MessageReceiverIdentifier");
                        XmlElement MessageReceiverName = newXmlDoc.CreateElement("MessageReceiverName");
                        XmlElement MessageTypeIdentifier = newXmlDoc.CreateElement("MessageTypeIdentifier");
                        XmlElement DocumentNumber = newXmlDoc.CreateElement("DocumentNumber");
                        XmlElement DocumentANSTime = newXmlDoc.CreateElement("DocumentANSDateTime");
                        XmlElement Status = newXmlDoc.CreateElement("Status");
                        XmlElement RefNo = newXmlDoc.CreateElement("RefNo");
                        XmlElement Remark = newXmlDoc.CreateElement("Remark");
                        if (xmlDoc.SelectSingleNode("WMSRECV") != null) {
                            MessageSenderIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/MessageSenderIdentifier").InnerText;
                            MessageSenderName.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/MessageSenderName").InnerText;
                            MessageReceiverIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/MessageReceiverIdentifier").InnerText;
                            MessageReceiverName.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/MessageReceiverName").InnerText;
                            MessageTypeIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/MessageTypeIdentifier").InnerText;
                            DocumentNumber.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/DocumentNumber").InnerText;
                            DocumentANSTime.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/DocumentDateTime").InnerText;
                            RefNo.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/InvoiceNo").InnerText;
                            Remark.InnerText = xmlDoc.SelectSingleNode("WMSRECV/Header/Remark").InnerText;
                        }
                        if (xmlDoc.SelectSingleNode("WMSRELS") != null) {
                            MessageSenderIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/MessageSenderIdentifier").InnerText;
                            MessageSenderName.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/MessageSenderName").InnerText;
                            MessageReceiverIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/MessageReceiverIdentifier").InnerText;
                            MessageReceiverName.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/MessageReceiverName").InnerText;
                            MessageTypeIdentifier.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/MessageTypeIdentifier").InnerText;
                            DocumentNumber.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/DocumentNumber").InnerText;
                            DocumentANSTime.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/DocumentDateTime").InnerText;
                            RefNo.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/InvoiceNo").InnerText;
                            Remark.InnerText = xmlDoc.SelectSingleNode("WMSRELS/Header/Remark").InnerText;
                        }
                        Status.InnerText = "FIN";
                        xe.AppendChild(MessageSenderIdentifier);
                        xe.AppendChild(MessageSenderName);
                        xe.AppendChild(MessageReceiverIdentifier);
                        xe.AppendChild(MessageReceiverName);
                        xe.AppendChild(MessageTypeIdentifier);
                        xe.AppendChild(DocumentNumber);
                        xe.AppendChild(DocumentANSTime);
                        xe.AppendChild(Status);
                        xe.AppendChild(RefNo);
                        xe.AppendChild(Remark);
                        if (xmlDoc.SelectSingleNode("WMSRECV") != null)
                        {
                            newXmlDoc.Save(Dir + "\\out\\" + xmlDoc.SelectSingleNode("WMSRECV/Header/InvoiceNo").InnerText + "_REC.xml");
                            newXmlDoc.Save(Dir + "\\out\\" + xmlDoc.SelectSingleNode("WMSRECV/Header/InvoiceNo").InnerText + "_FIN.xml");
                        }
                        if (xmlDoc.SelectSingleNode("WMSRELS") != null)
                        {
                            newXmlDoc.Save(Dir + "\\out\\" + xmlDoc.SelectSingleNode("WMSRELS/Header/InvoiceNo").InnerText + "_REC.xml");
                            newXmlDoc.Save(Dir + "\\out\\" + xmlDoc.SelectSingleNode("WMSRELS/Header/InvoiceNo").InnerText + "_FIN.xml");
                        }
                        listView1.Items.Add(dChild.FullName + "========【成功】");
                        listView1.LabelWrap = true;
                        listView1.EnsureVisible(i);
                        i++;
                    }
                    catch {
                        listView1.Items.Add(dChild.FullName + "========【失败】");
                        listView1.LabelWrap = true;
                        listView1.EnsureVisible(i);
                        i++;
                    }
                    // Response.Write(dChild.Name + "<BR>");//打印文件名
                    // Response.Write(dChild.FullName + "<BR>");//打印路径和文件名
                }
           } catch {
               MessageBox.Show("第一步要先选路径!");
           }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                if (!Directory.Exists(cDir + "\\out")){
                    Directory.CreateDirectory(cDir + "\\out");
                }
                DirectoryInfo dir = new DirectoryInfo(@"" + cDir + "");
                XmlDocument newXmlDoc = new XmlDocument();
                newXmlDoc.AppendChild(newXmlDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                XmlElement xe = newXmlDoc.CreateElement("WMSRELSLIST");
                newXmlDoc.AppendChild(xe);
                progressBar1.Maximum = dir.GetFiles("*.xml").Length;
                foreach (FileInfo dChild in dir.GetFiles("*.xml")){
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(dChild.FullName);
                    XmlElement node = xmlDoc.DocumentElement;
                    XmlNode xnDesi = newXmlDoc.ImportNode(xmlDoc.DocumentElement, true);
                    newXmlDoc.DocumentElement.AppendChild(xnDesi);
                    progressBar1.Value += progressBar1.Step;
                }
                newXmlDoc.Save(cDir + "\\out\\out.xml");
                newXmlDoc.Save(cDir + "\\out\\out.xls");
            }
            catch{
                MessageBox.Show("第一步要先选路径!");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(rDir + "\\out"))
                {
                    Directory.CreateDirectory(rDir + "\\out");
                }
                DirectoryInfo dir = new DirectoryInfo(@"" + rDir + "");
                int i = 0;
                XmlDocument newXmlDoc = new XmlDocument();
                newXmlDoc.AppendChild(newXmlDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                XmlElement xe = newXmlDoc.CreateElement("WMSRELSLIST");
                newXmlDoc.AppendChild(xe);
                foreach (FileInfo dChild in dir.GetFiles("*.xml"))
                {
                    i++;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(dChild.FullName);
                    XmlElement node = xmlDoc.DocumentElement;
                    XmlNode xnDesi = newXmlDoc.ImportNode(xmlDoc.DocumentElement, true);
                    newXmlDoc.DocumentElement.AppendChild(xnDesi);
                }
                newXmlDoc.Save(rDir + "\\out\\out.xml");
                newXmlDoc.Save(rDir + "\\out\\out.xls");
            }
            catch
            {
                MessageBox.Show("第一步要先选路径!");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择出库文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.label4.Text = "已选择文件夹:" + foldPath;
                cDir = foldPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择入库文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.label5.Text = "已选择文件夹:" + foldPath;
                rDir = foldPath;
            }
        }

       
    }
}

