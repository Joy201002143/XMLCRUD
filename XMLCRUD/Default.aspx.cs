using System;
using System.Web.UI;
using System.Data;
using System.Xml;   
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace XMLCRUD
{
    
    public partial class _Default : Page
    {
        private string xmlFilePath;
        protected void Page_Load(object sender, EventArgs e)
        {
            xmlFilePath = Server.MapPath("~/App_Data/data.xml");
            if (!IsPostBack)
            {
                LoadEmployees();
                BindDropdown();
                ReloadDrop();
                Session["LastSelection"] = DropDownList1.SelectedValue; 
            }

        }

        private void BindDropdown()
        {
            string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";  // Adjust the connection string

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "GetDistinctDesignation";  

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            List<string> designations = new List<string>();

                            while (reader.Read())
                            {       

                                designations.Add(reader["Designation"].ToString());
                            }

                            DropDownList1.DataSource = designations;
                            DropDownList1.DataBind();
                        }

                        DropDownList1.Items.Insert(0, new ListItem("-- View ALl --", "0"));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


        private void LoadEmployees()    
        {
            string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";
            string procedureName = "FEmployeeXML"; //Apply the procedure

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {       
                        cmd.CommandType = CommandType.StoredProcedure; //EXEC the store procedure
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("Id");         //Fetch the data tables Columns naems
                        dataTable.Columns.Add("Name");
                        dataTable.Columns.Add("Designation");
                        dataTable.Columns.Add("Salary");

                        while (reader.Read()) 
                        {
                            string xmlData = reader["EmPXml"].ToString(); //Read the XML stored in database

                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xmlData);

                            XmlNodeList employees = doc.GetElementsByTagName("Employee"); //Read main parrent tag of the database
                            foreach (XmlNode employee in employees)
                            {
                                DataRow row = dataTable.NewRow();
                                row["ID"] = reader["Id"];   //Read Every row of the database
                                row["Name"] = employee["Name"]?.InnerText;
                                row["Designation"] = employee["Designation"]?.InnerText;
                                row["Salary"] = employee["Salary"]?.InnerText;

                                dataTable.Rows.Add(row);
                            }
                        }

                        gvEmployees.DataSource = dataTable;
                        gvEmployees.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";
            string procedureName = "InsertEMP";  // Procedure name

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", txtName.Text);        // Name input from TextBox
                        cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);  // Designation input from TextBox
                        cmd.Parameters.AddWithValue("@Salary", decimal.Parse(txtSalary.Text));  // Salary input from TextBox


                        cmd.ExecuteNonQuery();
                    }
                }

                //LoadEmployees();
                Session["LastSelection"] = DropDownList1.SelectedValue;
                BindDropdown();
                ReloadDrop();
                ClearFields();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNode employee = doc.SelectSingleNode($"/Employees/Employee[ID='{txtID.Text}']");
            if (employee != null)
            {  
                employee["Name"].InnerText = txtName.Text;
                employee["Department"].InnerText = txtDesignation.Text;

                doc.Save(xmlFilePath);
                //LoadEmployees();
                ClearFields();
            }
        }


        //protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvEmployees.EditIndex = e.NewEditIndex;
        //    ReloadDrop();
        //}

        //protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = gvEmployees.Rows[e.RowIndex];

        //    TextBox txtEmployeeID = (TextBox)row.FindControl("txtEmployeeID");
        //    TextBox txtName = (TextBox)row.FindControl("txtName");
        //    TextBox txtDesignation = (TextBox)row.FindControl("txtDesignation");
        //    TextBox txtSalary = (TextBox)row.FindControl("txtSalary");

        //    int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

        //    UpdateEmployee(employeeID, txtName.Text, txtDesignation.Text, Convert.ToDecimal(txtSalary.Text));

        //    gvEmployees.EditIndex = -1;
        //    ReloadDrop();
        //}




        //private void UpdateEmployee(int employeeID, string name, string designation, decimal salary)
        //{
        //    string connString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        SqlCommand cmd = new SqlCommand("UpdateEmployee", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
        //        cmd.Parameters.AddWithValue("@Name", name);
        //        cmd.Parameters.AddWithValue("@Designation", designation);
        //        cmd.Parameters.AddWithValue("@Salary", salary);

        //        conn.Open();

        //        cmd.ExecuteNonQuery();
        //    }
        //}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";
            string procedureName = "DelEmployee";  

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", int.Parse(txtDel.Text));

                        cmd.ExecuteNonQuery();
                    }
                }

                //LoadEmployees();
                ReloadDrop();


                ClearFields();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    //DataSet ds = new DataSet();
        //    //ds.ReadXml(xmlFilePath);

        //    //DataTable employeeTable = ds.Tables["Employee"];
        //    //if (employeeTable != null)
        //    //{
        //    //    DataView dv = new DataView(employeeTable);
        //    //    dv.RowFilter = $"Department LIKE '%{txtSearch.Text}%' OR ID LIKE '%{txtSearch.Text}%' OR Name LIKE '%{txtSearch.Text}%'";
        //    //    gvEmployees.DataSource = dv;
        //    //    gvEmployees.DataBind();
        //    //    ClearFields();
        //    //}
        //}
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }
        protected void ReloadDrop()
        {
           
                string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";
                string procedureName = "SearchEmployeeByDesignations"; // The procedure we created

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Get the selected value from the DropDownList
                        string selectedValue="0";
                        if (!string.IsNullOrEmpty(Session["LastSelection"] as string) )
                            {
                            selectedValue = Session["LastSelection"] as string;
                        }

                        // If "0" is selected, load all employees (or fetch all records)
                        if (selectedValue == "0")
                        {
                            LoadEmployees(); // This can be your existing function to load all employees without any filter
                        }
                        else
                        {   
                            cmd.Parameters.AddWithValue("@Designation", selectedValue);

                            SqlDataReader reader = cmd.ExecuteReader();
                            DataTable employeeTable = new DataTable();
                            employeeTable.Columns.Add("Id");
                            employeeTable.Columns.Add("Name");
                            employeeTable.Columns.Add("Designation");
                            employeeTable.Columns.Add("Salary");

                            while (reader.Read())
                            {
                                DataRow row = employeeTable.NewRow();
                                row["Id"] = reader["Id"];
                                row["Name"] = reader["Name"];
                                row["Designation"] = reader["Designation"];
                                row["Salary"] = reader["Salary"];

                                employeeTable.Rows.Add(row);
                            }

                            gvEmployees.DataSource = employeeTable;
                            gvEmployees.DataBind();
                        }
                    }
                }
            }
                    


            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }

        
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)   
        {
            string connectionString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";
            string procedureName = "SearchEmployeeByDesignations"; // The procedure we created

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string selectedValue;
                        // Get the selected value from the DropDownList
                        if (!string.IsNullOrEmpty(Session["LastSelection"] as string))
                        {
                            selectedValue = Session["LastSelection"] as string;
                        }
                        
                            selectedValue= DropDownList1.SelectedValue;
                        Session["LastSelection"] = selectedValue;

                        //  DropDownList1.SelectedValue;

                        // If "0" is selected, load all employees (or fetch all records)
                        if (selectedValue == "0")
                        {
                            LoadEmployees(); // This can be your existing function to load all employees without any filter
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Designation", selectedValue);

                            SqlDataReader reader = cmd.ExecuteReader();
                            DataTable employeeTable = new DataTable();
                            employeeTable.Columns.Add("Id");
                            employeeTable.Columns.Add("Name");
                            employeeTable.Columns.Add("Designation");
                            employeeTable.Columns.Add("Salary");

                            while (reader.Read())
                            {
                                DataRow row = employeeTable.NewRow();
                                row["Id"] = reader["Id"];
                                row["Name"] = reader["Name"];
                                row["Designation"] = reader["Designation"];
                                row["Salary"] = reader["Salary"];

                                employeeTable.Rows.Add(row);
                            }

                            gvEmployees.DataSource = employeeTable;
                            gvEmployees.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex; // Set the row to Edit mode
            ReloadDrop(); // Reload data to reflect editable rows
        }
        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)   
        {
            gvEmployees.EditIndex = -1; // Reset EditIndex to disable Edit mode
            ReloadDrop(); // Reload data to refresh the GridView
        }
        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the row being edited
            GridViewRow row = gvEmployees.Rows[e.RowIndex];

            // Retrieve values from the controls
            int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value); // DataKey is "Id"
            string name = ((TextBox)row.Cells[1].Controls[0]).Text; // Edit control for Name
            string designation = ((TextBox)row.Cells[2].Controls[0]).Text; // Edit control for Designation
            decimal salary = decimal.Parse(((TextBox)row.Cells[3].Controls[0]).Text); // Edit control for Salary

            // Update the database record
            UpdateEmployee(employeeID, name, designation, salary);

            gvEmployees.EditIndex = -1; // Exit edit mode
            ReloadDrop(); // Refresh data
        }
        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the ID of the row being deleted
            int employeeID = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

            // Call the delete method
            DeleteEmployee(employeeID);

            ReloadDrop(); // Refresh data
        }

        private void UpdateEmployee(int employeeID, string name, string designation, decimal salary)
        {
            string connString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployeeDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", employeeID);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@Salary", salary);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteEmployee(int employeeID)
        {
            string connString = "Server=DESKTOP-JHB8AON;Database=Employee;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("sp_DelEmployee_s", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", employeeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }

}