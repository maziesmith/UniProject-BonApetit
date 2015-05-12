using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BonApetit.Controls.Forms
{
    public partial class DynamicTextBoxAdditionalEntry : System.Web.UI.UserControl
    {
        private int index;

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
                this.ID = "AdditionalEntry_" + this.index;
            }
        }

        public string Text
        {
            get
            {
                return this.Entry.Text;
            }
            set
            {
                this.Entry.Text = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RemoveAdditionalEntry_ServerClick(object sender, EventArgs e)
        {
            ((DynamicTextBox)this.Parent.Parent).RemoveAdditionalEntry(this.Index, this);
        }
    }
}