using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FugasDetectionSystem.SQLGeneratorTools;




namespace FugasDetectionSystem.SQLGeneratorTools
{
    public partial class FormMain : Form
    {
        SQLExecutor sqlExecutor = new SQLExecutor();
        private static readonly string FILE_PATH = "path_to_your_file.json";
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            if (File.Exists(FILE_PATH))
            {
                // Si el archivo existe, carga la estructura del TreeView desde el archivo JSON
                LoadTreeViewFromJson(treeView1, FILE_PATH);
            }
            else
            {
                CreateTreeViewFromDatabaseAsync(FILE_PATH);
            }
        }




        private void LoadTreeViewFromJson(System.Windows.Forms.TreeView treeView, string filePath)
        {
            treeView.Nodes.Clear();

            string json = File.ReadAllText(filePath);
            var treeData = DeserializeJson(json);

            foreach (var table in treeData)
            {
                TreeNode tableNode = new TreeNode(table.TableName);
                foreach (var procName in table.StoredProcedures)
                {
                    TreeNode procedureNode = new TreeNode(procName);
                    tableNode.Nodes.Add(procedureNode);
                }
                treeView.Nodes.Add(tableNode);
            }
        }


        public static List<TableData> DeserializeJson(string json)
        {
            return JsonSerializer.Deserialize<List<TableData>>(json);
        }



        private void SaveTreeViewToJson(System.Windows.Forms.TreeView treeView, string filePath)
        {
            var treeData = treeView.Nodes.Cast<TreeNode>()
                              .Select(node => new
                              {
                                  TableName = node.Text,
                                  StoredProcedures = node.Nodes.Cast<TreeNode>()
                                                      .Select(n => n.Text).ToList()
                              }).ToList();

            string json = JsonSerializer.Serialize(treeData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTreeViewFromDatabaseAsync(FILE_PATH); // Ahora es una tarea asincrónica
        }

        private async void CreateTreeViewFromDatabaseAsync(string filePath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; // Establece el cursor de espera
                treeView1.BeginUpdate(); // Mejora el rendimiento al agregar nodos al TreeView
                treeView1.Nodes.Clear();

                // Carga asincrónicamente la estructura del TreeView desde la base de datos
                var tables = await sqlExecutor.GetTablesAsync(); // Suponiendo que GetTables tiene una versión asincrónica

                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    TreeNode tableNode = new TreeNode(tableName);

                    // De nuevo, esperamos que exista una versión asincrónica de GetStoredProcedures
                    var procedures = await sqlExecutor.GetStoredProceduresAsync(tableName);

                    foreach (DataRow procRow in procedures.Rows)
                    {
                        string procedureName = procRow["name"].ToString();
                        TreeNode procedureNode = new TreeNode(procedureName);
                        tableNode.Nodes.Add(procedureNode);
                    }

                    treeView1.Nodes.Add(tableNode);
                }

                SaveTreeViewToJson(treeView1, filePath); // Esto puede seguir siendo síncrono a menos que quieras cambiarlo también

                treeView1.EndUpdate(); // Finaliza la actualización del TreeView
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción aquí
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default; // Restablece el cursor
                                                  // Habilita nuevamente cualquier control que haya sido deshabilitado
            }
        }

        private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                // Solo proceder si el nodo seleccionado es una tabla.
                try
                {
                    string selectedTableName = e.Node.Text;
                    var columnDetailsList = await sqlExecutor.GetTableColumnsAsync(selectedTableName);
                    DGVColumnas.DataSource = columnDetailsList; // Asumiendo que tu DataGridView se llama dgvColumnas
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al cargar columnas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


