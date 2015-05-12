using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BonApetit.Controls.Forms
{
    public partial class DynamicTextBox : System.Web.UI.UserControl
    {
        #region Properties

        public string Title { get; set; }

        protected List<int> AdditionalEntriesIds
        {
            get
            {
                if (!(ViewState[AdditionalEntriesIds_Name] is List<int>))
                {
                    ViewState[AdditionalEntriesIds_Name] = new List<int>();
                }

                return (List<int>)ViewState[AdditionalEntriesIds_Name];
            }
        }

        protected int MaxAdditionalEntryId
        {
            get
            {
                if (!(ViewState[MaxAdditionalEntryId_Name] is int))
                    ViewState[MaxAdditionalEntryId_Name] = 0;

                return (int)ViewState[MaxAdditionalEntryId_Name];
            }
            set
            {
                ViewState[MaxAdditionalEntryId_Name] = value;
            }
        }

        #endregion

        public void AddAdditionalEntry(int index)
        {
            this.AdditionalEntriesIds.Add(index);
            CreateAdditionalEntry(index);
        }

        public void RemoveAdditionalEntry(int index, DynamicTextBoxAdditionalEntry entry)
        {
            this.AdditionalEntriesIds.Remove(index);
            this.AdditionalEntries.Controls.Remove(entry);
        }

        public IEnumerable<string> GetAllValues()
        {
            var values = new List<string>();

            if (!string.IsNullOrWhiteSpace(this.MainEntry.Text))
                values.Add(this.MainEntry.Text);

            foreach (var additionalEntry in this.AdditionalEntries.Controls)
            {
                var entryValue = ((DynamicTextBoxAdditionalEntry)additionalEntry).Text;
                if (!string.IsNullOrWhiteSpace(entryValue) && !values.Contains(entryValue, StringComparer.InvariantCultureIgnoreCase))
                    values.Add(entryValue);
            }

            return values;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ControlLabel.Text = this.Title;
            }
            else
            {
                //Controls must be repeatedly be created on postback
                this.CreateControls();
            }
        }

        protected void Page_Render(object sender, EventArgs e)
        { 
        
        }

        protected void CreateControls()
        {
            foreach(var id in this.AdditionalEntriesIds)
            {
                this.CreateAdditionalEntry(id);
            }
        }

        protected void AddEntryButton_Click(object sender, EventArgs e)
        {
            var additionaEntryId = ++this.MaxAdditionalEntryId;
            this.AddAdditionalEntry(additionaEntryId);
        }

        private void CreateAdditionalEntry(int index)
        {
            var additionalEntry = Page.LoadControl("~/Controls/Forms/DynamicTextBoxAdditionalEntry.ascx") as DynamicTextBoxAdditionalEntry;
            additionalEntry.Index = index;
            AdditionalEntries.Controls.Add(additionalEntry);
        }

        private const string AdditionalEntriesIds_Name = "AdditionalEntriesIds";
        private const string MaxAdditionalEntryId_Name = "MaxAdditionalEntryId";
    }
}