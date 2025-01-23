using System; 
using System.Web.UI; 

namespace exam
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                   
                }
                 
            }
            catch (Exception ex)
            {

            }
        }
    }
}