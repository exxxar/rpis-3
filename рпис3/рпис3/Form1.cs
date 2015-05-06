using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace рпис3
{
    public partial class Form1 : Form
    {

        String path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
          
            statusStrip1.Items[0].Text = " Size : " + fullSize(this.treeView1.SelectedNode.FullPath);

            listView1.Items.Clear();
            try
            {
                string[] str = Directory.GetFiles(treeView1.SelectedNode.FullPath);
                path = treeView1.SelectedNode == null ? "" : treeView1.SelectedNode.FullPath;
                foreach (var str1 in str)
                {

                    ListViewItem item1 = new ListViewItem("");
                    item1.Checked = true;
                    item1.SubItems.Add(str1.Substring(str1.LastIndexOf("\\") + 1));
                    item1.SubItems.Add((str1.Substring(str1.LastIndexOf(".") + 1)).Length > 4 ? "" : str1.Substring(str1.LastIndexOf(".") + 1));
                    item1.SubItems.Add("");

                    listView1.Items.AddRange(new ListViewItem[] { item1 });
                    statusStrip1.Items[1].Text = "Files selected " + checkCount().ToString() + "\\" + filesCount();
                    //statusStrip1.Items[0].Text = " Size : " + fullSize(this.treeView1.SelectedNode.FullPath);
                }

            }

            catch (Exception e1)
            {
                label1.Text = e1.Message;
            }
            Graph();
            ChangeColor();
            // statusStrip1.Items[0].Text = " Size : " + fullSize(this.treeView1.SelectedNode.FullPath);  
            statusStrip1.Items[1].Text = "Files selected " + checkCount().ToString() + "\\" + filesCount();
        }

        private void treeView1_Load(object sender, System.EventArgs e)
        { }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        { }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {  }

        private void label1_Click(object sender, EventArgs e)
        {  }

        private void button1_Click(object sender, EventArgs e)
        { }

        private void button2_Click(object sender, EventArgs e)
        { }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            comboBox1.DataSource = Environment.GetLogicalDrives();
           var path = comboBox1.SelectedItem;
         //  statusStrip1.Items[0].Text = " Size : " + fullSize(this.treeView1.SelectedNode.FullPath);  
           // TreeCreate(path.ToString(),2);
    }
        protected void TreeCreate(string path, int k)
        {
            k--;
            
                treeView1.Nodes.Clear();
                string sPath = path; // Указываем корневой каталог
                DirectoryInfo DirInfo;
                DirInfo = new DirectoryInfo(sPath);
                DirectoryInfo[] Dirs = DirInfo.GetDirectories(); // Функция GetDirectories() возвращает подкаталоги текущего каталога в виде
                // массива элементов типа DirectoryInfo
                treeView1.BeginUpdate(); // Отключает перерисовку компонента TreeView
                treeView1.Nodes.Clear(); // Очищает компонент TreeView
                treeView1.Nodes.Add(new TreeNode(sPath)); // Занесение корневого каталога
                // Рекурсивная функция заносящая информацию о каталогах в компонент (TreeView)
                if (k != 0)
                { GetDir_AddInTree(sPath, treeView1.Nodes[0], k); }
               
                treeView1.EndUpdate(); // Включает перерисовку компонента TreeView
                
            //statusStrip1.Items[1].Text = "Files selected " + checkCount().ToString() + "\\" + filesCount();
        }

        protected void GetDir_AddInTree(string sDir, TreeNode NodeCollection, int k)
        {
            k--;
            try
            {
                DirectoryInfo DirInfo = new DirectoryInfo(sDir);

                DirectoryInfo[] Dirs = DirInfo.GetDirectories();
                int i = 0;

                foreach (DirectoryInfo dri in Dirs)
                {
                    NodeCollection.Nodes.Add(new TreeNode(dri.Name));

                    GetDir_AddInTree(sDir + dri.Name + "\\", NodeCollection.Nodes[i],k);
                    i++;
                    //listView1.Items.Add(dri.Name);
                }
            }
            catch (Exception e1)
            {
                label1.Text = e1.Message;
            }
        }
        private void treeView1_Click(object sender, EventArgs e)
        {
           
        }
       
    

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  TreeCreate(path);
        }

        private void label2_Click(object sender, EventArgs e)
        { }
        protected void Graph()
        {
            chart1.Series["Series1"].Points.Clear();
            Console.WriteLine(path);
            int h = 0, g = 0, f = 0;
            try
            {

                string[] str = Directory.GetFiles(path);
                foreach (var str1 in str)
                {

                    var len = str1.Substring(str1.LastIndexOf("\\") + 1).Length;
                    foreach (ListViewItem v in listView1.Items)
                    {
                        
                        if (v.Checked && v.SubItems[1].Text.Equals(str1.Substring(str1.LastIndexOf("\\") + 1)))
                        {
                           

                            if (len >= 1 && len <= 15)
                            {
                                h++;
                                
                            }
                            if (len > 15 && len <= 20)
                            {
                                g++;
                               
                            }
                            if (len > 20)
                            {
                                f++;
                              
                            }

                        }
                    }
                    string[] ColumnName = { "Малые", "Cредние", "Большие" };
                    int[] chet = { h,g,f };
                    chart1.Series["Series1"].Points.DataBindXY(ColumnName, chet);

                }
            }
            catch (Exception e1)
            {
                label1.Text = e1.Message;
            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        { 
            try 
            {
                var path = comboBox1.SelectedItem;
             
                TreeCreate(path.ToString(), 2);
                 }
            catch (Exception e3)
            {
                label2.Text = e3.Message;
            }
        }

     
           protected void SaveTree(TreeNodeCollection Nodes,StreamWriter sw = null,String itterator = "+")
        {
            if (sw == null)
            sw = File.CreateText("first.txt");
           // sw.WriteLine("Начало:");
            foreach (TreeNode a in Nodes)
            {
                sw.WriteLine(itterator + a.Text);
                SaveTree2(a.Nodes,sw);
           }
            sw.Flush();
            sw.Close();
        }

        protected void SaveTree2(TreeNodeCollection Nodes, StreamWriter sw = null,String itterator = "+")
        {
          itterator += itterator.Substring(0,1);
          foreach (TreeNode a in Nodes)
            {
                sw.WriteLine(itterator+a.Text);
                SaveTree2(a.Nodes, sw,itterator);

            }

        }

       private void fileToolStripMenuItem_Click(object sender, EventArgs e)
       {

       }

       private void saveToolStripMenuItem_Click(object sender, EventArgs e)
       {
           SaveTree(treeView1.Nodes);
       }

       private void openToolStripMenuItem_Click(object sender, EventArgs e)
       {
           var path = textBox1.Text;

           TreeCreate(path.ToString(), 3);
       }

       private void textBox1_MouseHover(object sender, EventArgs e)
       {
       }

       private void fontDialog1_Apply(object sender, EventArgs e)
       {

       }

       private void fontToolStripMenuItem_Click(object sender, EventArgs e)
       {
           Font font;
          
           DialogResult result = fontDialog1.ShowDialog();
           font = fontDialog1.Font;
           listView1.Font = font;
           //foreach (Control ctrl in Form1.ActiveForm.Controls)
           //{
           //    ctrl.Font = font;
           //}
          
       }
      


       private void colorToolStripMenuItem_Click(object sender, EventArgs e)
       { 
          }
        protected void ChangeColor()
       {
         String[] fileExt = { "png", "jpg", "doc", "rar","exe","dll","txt","docx" };
         Color[] extColor = { Color.Red, Color.Pink, Color.Blue, Color.Green, Color.Brown,Color.DarkOrange, Color.DimGray,Color.Indigo};
            for (int i=0;i<fileExt.Length;i++)
            {
                foreach (ListViewItem ctrl in listView1.Items)
                {
                    string str = ctrl.SubItems[2].Text;
                    if (fileExt[i].Equals (str.Substring(str.LastIndexOf(".") + 1)))
                    {
                        ctrl.BackColor = extColor[i];
                    
                      } 
                }
            }


       }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void exeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(4);
        }
        protected void ChangeFormatColor(int i)
        {
            String[] fileExt = { "png", "jpg", "iso", "rar", "exe", "dll", "txt", "docx" };
            Color color;
            DialogResult result = colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            foreach (ListViewItem ctrl in listView1.Items)
            {
                string str = ctrl.SubItems[2].Text;
                if (fileExt[i].Equals(str.Substring(str.LastIndexOf(".") + 1)))
                {
                    ctrl.BackColor = color;

                }
            }
        }

        private void dllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(5);
        }

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(6);
        }

        private void docxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(7);
        }

        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(0);
        }

        private void jpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(1);
        }

        private void isoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(2);
        }

        private void rarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFormatColor(3);
        }
          public int checkCount()
            {
            int sum = 0;
            foreach (ListViewItem v in listView1.Items)
            {
            if (v.Checked)
            sum++;
            }
            return sum;
            }

        public int filesCount()
            {
            return listView1.Items.Count;
            }
        public Int64 fullSize(String path = "")
        {
            Int64 sum = 0;
            try
            {

                foreach (var s in Directory.GetFiles(path))
                {
                    FileInfo file = new FileInfo(s);
                    sum += file.Length;
                }

            }
            catch (Exception e)
            {
                return 0;
            }
            return sum;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            Graph();
            statusStrip1.Items[1].Text = "Files selected " + checkCount().ToString() + "\\" + filesCount();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
        }
        
       }
    }
    



    

