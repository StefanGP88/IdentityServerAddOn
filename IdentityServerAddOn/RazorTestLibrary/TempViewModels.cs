using System;

namespace RazorTestLibrary
{
    public class PropertRowModel
    {
        public string Property { get; set; }
        public string PropertyKey { get; set; }
    }

    public class SecretRowModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }

    public class AddRoleClaimModel
    {
        public string Claim { get; set; }
        public string ClaimType { get; set; }
    }

    public class PartialDataContainer
    {
        public PartialDataContainer(string resource, string property, bool isForExistingResource, bool isDetailPartial, bool isAddForm)
        {
            Resource = resource;
            IsForExistingResource = isForExistingResource;
            IsDetailPartial = isDetailPartial;
            Property = property;
            IsAddForm = isAddForm;
        }
        public string Resource { get; set; }
        public string Property { get; set; }
        public bool IsForExistingResource { get; set; }
        public bool IsDetailPartial { get; set; }
        public bool IsAddForm { get; set; }
        public string FormType { get { return IsAddForm ? "AddForm" : "EditForm"; } }
        public string GetHtmlId(string name)
        {
            return (IsForExistingResource, IsDetailPartial) switch
            {
                (true, true) => string.Format("see{0}DetailForExisting{1}{2}", name, Resource, FormType),
                (true, false) => string.Format("add{0}ToExisting{1}{2}", name, Resource, FormType),
                (false, true) => string.Format("see{0}DetailForNew{1}{2}", name, Resource, FormType),
                (false, false) => string.Format("add{0}ToNew{1}{2}", name, Resource, FormType)
            };
        }

        public string GeHtmlTags()
        {
            return IsDetailPartial ? " disabled " : string.Empty;
        }

        public string GetPopulateMethodName(string name)
        {
            if (IsForExistingResource) return string.Format("Populate{0}ForExisitng{1}(element)", name, FormType);
            return string.Format("Populate{0}ForNew{1}(element)", name, FormType);
        }
        public string GetUnpopulateMethodName(string name)
        {
            if (IsForExistingResource) return string.Format("Populate{0}ForExisitng{1}(element)", name, FormType);
            return string.Format("Unopulate{0}ForNew{1}(element)", name, FormType);
        }

        public string GetTabId(string tabName, TabType tabType)
        {
            return string.Format("nav-{0}{1}-tab", tabName.Replace(" ", ""), FormType).ToLower();
        }

        public string GetNavTargetId(string tabName, TabType tabType)
        {
            return string.Format("nav-{0}{1}", tabName.Replace(" ", ""), FormType).ToLower();
        }
    }

    public class TablePartialDataContainer : PartialDataContainer
    {
        public TablePartialDataContainer(string resource, string property, bool isForExistingResource, bool isAddForm, string[] columns)
            : base(resource, property, isForExistingResource, false, isAddForm)
        {
            Columns = columns;
        }

        public string[] Columns { get; set; }

        public string GetAddButtonId()
        {
            if (IsForExistingResource) return string.Format("add{0}ToExisitng{1}Btn{2}", Property, Resource, FormType);
            return string.Format("add{0}ToNew{1}Btn{2}", Property, Resource, FormType);
        }
    }

    public class FormPartialDataContainer : PartialDataContainer
    {
        public FormPartialDataContainer(string resource, string property, bool isForExistingResource, bool isAddForm)
            : base(resource, property, isForExistingResource, false, isAddForm) { }
    }
    public class NavTabDataContainer
    {
        public bool IsActive { get; set; }
        public string TabName { get; set; }
        public bool IsAddForm { get; set; }
        public string FormType { get { return IsAddForm ? "AddForm" : "EditForm"; } }

        public NavTabDataContainer(string tabName, bool isActive, bool isAddForm)
        {
            IsActive = isActive;
            TabName = tabName;
            IsAddForm = isAddForm;
        }

        public string GetTabId(TabType tabType)
        {
            return string.Format("nav-{0}{1}-tab", TabName.Replace(" ", ""), FormType).ToLower();
        }

        public string GetNavTargetId(TabType tabType)
        {
            return string.Format("nav-{0}{1}", TabName.Replace(" ", ""), FormType).ToLower();
        }
    }

    public enum TabType
    {
        Overview,
        Add,
        Detail
    }
}
