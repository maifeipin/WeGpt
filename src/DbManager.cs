using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WEBGPT.Properties;

namespace WEBGPT
{
    public partial class DbManager : Form
    {
        public SQLiteConnection connection { get; set; }
        public string ClientProxy { get; set; } 
        public DbManager()
        {
            InitializeComponent();
        }
        public DbManager(SQLiteConnection _con) : this()
        {
            connection= _con;
        }
        private void DbManager_Load(object sender, EventArgs e)
        {
            if(connection == null) { return; }
            string query = "SELECT * FROM url";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            if (connection.State != ConnectionState.Open) connection.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "ID";
            idColumn.HeaderText = "ID"; // 列标题
            idColumn.DataPropertyName = "ID"; // 数据库中对应的字段名
            idColumn.Visible = false; // 将该列隐藏
            dataGridView1.Columns.Add(idColumn);

           
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            textColumn.Name = "url";
            textColumn.HeaderText = "链接";
            textColumn.DataPropertyName = "url";  
            dataGridView1.Columns.Add(textColumn);
            
            DataGridViewTextBoxColumn memoColumn = new DataGridViewTextBoxColumn();
            memoColumn.Name = "memo";
            memoColumn.HeaderText = "备注";
            memoColumn.DataPropertyName = "memo";
            dataGridView1.Columns.Add(memoColumn);

             
            DataGridViewLinkColumn linkEditorColumn = new DataGridViewLinkColumn();
            linkEditorColumn.Name = "save";
            linkEditorColumn.HeaderText = "操作";
            linkEditorColumn.Text = "保存";  
            linkEditorColumn.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(linkEditorColumn);
              
            DataGridViewLinkColumn linkDelColumn = new DataGridViewLinkColumn();
            linkDelColumn.Name = "del";
            linkDelColumn.HeaderText = "操作";
            linkDelColumn.Text = "删除";
            linkDelColumn.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(linkDelColumn);
             
            DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            linkColumn.Name = "detail";
            linkColumn.HeaderText = "操作";
            linkColumn.Text = "详情";
            linkColumn.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(linkColumn);

            // 订阅 CellContentClick 事件以处理链接点击
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;


            dataGridView1.DataSource = dataTable;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["del"].Index && e.RowIndex >= 0)
            { 
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string id = selectedRow.Cells["id"].Value.ToString();

                string sql = " delete from url where id=" + id;

                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                if (connection.State != ConnectionState.Open) connection.Open();
                int exec  = cmd.ExecuteNonQuery();
                if (exec == 1)
                    MessageBox.Show($"删除成功");
            }
            if (e.ColumnIndex == dataGridView1.Columns["detail"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string uri = selectedRow.Cells["url"].Value.ToString();
                WebViewer webViewer = new WebViewer(uri,ClientProxy); 
                webViewer.Show();
            } 
            if (e.ColumnIndex == dataGridView1.Columns["save"].Index && e.RowIndex >= 0)
            { 
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string id = selectedRow.Cells["id"].Value.ToString();
                string url = selectedRow.Cells["url"].Value.ToString();
                string memo = selectedRow.Cells["memo"].Value.ToString();
                string sql = $" update  url set url='{url}',memo='{memo}' where id={id}" ;

                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                if (connection.State != ConnectionState.Open) connection.Open();
                int exec = cmd.ExecuteNonQuery();
                if (exec == 1)
                    MessageBox.Show($"保存成功");
            }
        }
    }
}
