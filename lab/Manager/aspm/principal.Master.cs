using System;


namespace lab.Manager.aspm
{
    public partial class principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Session["usuario"] == null)
                {
                   
                    Response.Redirect("~/Default.aspx", false);

                }
            }
            catch
            {
                Response.Redirect("~/Default.aspx", false);
            }
        
        }
         
   
}
}