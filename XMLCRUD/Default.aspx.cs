using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.Web.UI.WebControls;

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

            }

        }

        private void BindDropdown()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);

            if (ds.Tables["Employee"] != null)
            {
                DropDownList1.DataSource = ds.Tables["Employee"];
                DropDownList1.DataTextField = "Name";  
                DropDownList1.DataValueField = "ID";  
                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
            }
        }

        private void LoadEmployees()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);
            gvEmployees.DataSource = ds.Tables["Employee"];
            gvEmployees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlElement newEmployee = doc.CreateElement("Employee");

            XmlElement id = doc.CreateElement("ID");
            id.InnerText = txtID.Text;
            newEmployee.AppendChild(id);

            XmlElement name = doc.CreateElement("Name");
            name.InnerText = txtName.Text;
            newEmployee.AppendChild(name);

            XmlElement department = doc.CreateElement("Department");
            department.InnerText = txtDepartment.Text;
            newEmployee.AppendChild(department);

            doc.DocumentElement.AppendChild(newEmployee);
            doc.Save(xmlFilePath);

            LoadEmployees();
            ClearFields();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNode employee = doc.SelectSingleNode($"/Employees/Employee[ID='{txtID.Text}']");
            if (employee != null)
            {
                employee["Name"].InnerText = txtName.Text;
                employee["Department"].InnerText = txtDepartment.Text;

                doc.Save(xmlFilePath);
                LoadEmployees();
                ClearFields();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNode employee = doc.SelectSingleNode($"/Employees/Employee[ID='{txtID.Text}']");
            if (employee != null)
            {
                doc.DocumentElement.RemoveChild(employee);
                doc.Save(xmlFilePath);

                LoadEmployees();
                ClearFields();

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);

            DataTable employeeTable = ds.Tables["Employee"];
            if (employeeTable != null)
            {
                DataView dv = new DataView(employeeTable);
                dv.RowFilter = $"Department LIKE '%{txtSearch.Text}%' OR ID LIKE '%{txtSearch.Text}%' OR Name LIKE '%{txtSearch.Text}%'";
                gvEmployees.DataSource = dv;
                gvEmployees.DataBind();
                ClearFields();
            }
        }
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDepartment.Text = string.Empty;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);

            DataTable employeeTable = ds.Tables["Employee"];
            if (employeeTable != null)
            {

                if (DropDownList1.SelectedValue == "0" ) {
                    LoadEmployees();
                }
                else
                {
                    DataView dv = new DataView(employeeTable);
                    dv.RowFilter = $"Department LIKE '%{DropDownList1.SelectedValue}%' OR ID LIKE '%{DropDownList1.SelectedValue}%' OR Name LIKE '%{DropDownList1.SelectedValue}%'";
                    gvEmployees.DataSource = dv;
                    gvEmployees.DataBind();
                }
                ClearFields();
            }

        }
    }
}