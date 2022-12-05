using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryException
{
    public partial class frmAddProduct : Form
    {

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();

        }
        public class NumberFormatException : Exception
        {

            public NumberFormatException(string qty) : base(qty) { }
        }
        public class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string price) : base(price) { }
        }
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

        private void btnAdd_Click(object sender, EventArgs e)
        {
                _ProductName = Product_Name(txtProduct.Text);
                _Category = cmbCategory.Text;
                _MfgDate = dtpMfg.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtpExp.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQty.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
        }
        BindingSource showProductList;

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[]
            {
                "Beverages", "Bread/Bakery", "Carned/Jarred Goods", "Dairy","Frozen Goods", "Meat" , "Personal Care", "Other"
            };
            foreach (string Category in ListOfProductCategory)
            {
                cmbCategory.Items.Add(Category);
            }
        }
        public string Product_Name (string name)
        {
            try
            {
                if (txtProduct.Text == "")
                {
                    MessageBox.Show("Product Name: is required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException(name);
                }
            }
            catch (StringFormatException sfe)
            {
                MessageBox.Show("String Format Only!\n" + sfe,"StringFormatException", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                return name;
        }
        public int Quantity(string qty)
        {
            try
            {
                if (txtQty.Text == "")
                {
                    MessageBox.Show("Quantity: is required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   

                }
                else if(!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException(qty);
                }
            }
            catch (NumberFormatException nfe)
            {
                MessageBox.Show("Number Format Only!\n" + nfe, "NumberFormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            
            try
            {
                if (txtSellPrice.Text == "")
                {
                    MessageBox.Show("Sell Price: is required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
                }
                else if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException(price);
                }
            }
            catch(CurrencyFormatException cfe)
            {
                MessageBox.Show("Number Format Only!\n" + cfe, "CurrencyFormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Convert.ToDouble(price);
        }
    }
}