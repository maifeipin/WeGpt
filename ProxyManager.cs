using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBGPT
{
    public partial class ProxyManager : Form
    {
        public SQLiteConnection connection { get; set; }
        private SQLiteDataAdapter adapter;
        private DataTable dataTable;
        public ProxyManager()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        private void DbManager_Load(object sender, EventArgs e)
        {
            if (connection == null) return;

            string query = "SELECT * FROM proxyserver";
            adapter = new SQLiteDataAdapter(query, connection);

            // 自动生成 INSERT/UPDATE/DELETE 命令
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);

            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow newRow = dataTable.NewRow();
            dataTable.Rows.Add(newRow);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                    dataGridView1.Rows.Remove(row);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataTable);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败: " + ex.Message);
            }
        }
    }
}
