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

    //public class PartialDataContainer
    //{
    //    public PartialDataContainer(string resource, string property, bool isForExistingResource, bool isDetailPartial, bool isAddForm)
    //    {
    //        Resource = resource;
    //        IsForExistingResource = isForExistingResource;
    //        IsDetailPartial = isDetailPartial;
    //        Property = property;
    //        IsAddForm = isAddForm;
    //    }
    //    public string Resource { get; set; }
    //    public string Property { get; set; }
    //    public bool IsForExistingResource { get; set; }
    //    public bool IsDetailPartial { get; set; }
    //    public string GetHtmlId(string name)
    //    {
    //        return (IsForExistingResource, IsDetailPartial) switch
    //        {
    //            (true, true) => string.Format("see{0}DetailForExisting{1}{2}", name, Resource, FormType),
    //            (true, false) => string.Format("add{0}ToExisting{1}{2}", name, Resource, FormType),
    //            (false, true) => string.Format("see{0}DetailForNew{1}{2}", name, Resource, FormType),
    //            (false, false) => string.Format("add{0}ToNew{1}{2}", name, Resource, FormType)
    //        };
    //    }

    //    public string GeHtmlTags()
    //    {
    //        return IsDetailPartial ? " disabled " : string.Empty;
    //    }

    //    public string GetPopulateMethodName(string name)
    //    {
    //        if (IsForExistingResource) return string.Format("Populate{0}ForExisitng{1}(element)", name, FormType);
    //        return string.Format("Populate{0}ForNew{1}(element)", name, FormType);
    //    }
    //    public string GetUnpopulateMethodName(string name)
    //    {
    //        if (IsForExistingResource) return string.Format("Populate{0}ForExisitng{1}(element)", name, FormType);
    //        return string.Format("Unopulate{0}ForNew{1}(element)", name, FormType);
    //    }

    //    public string GetTabId(string tabName, TabType tabType)
    //    {
    //        return string.Format("nav-{0}{1}-tab", tabName.Replace(" ", ""), FormType).ToLower();
    //    }

    //    public string GetNavTargetId(string tabName, TabType tabType)
    //    {
    //        return string.Format("nav-{0}{1}", tabName.Replace(" ", ""), FormType).ToLower();
    //    }
    //}


    public class FormPartialDataContainer
    {
        public FormPartialDataContainer(string resource, bool isAddForm)
        {
            IsAddForm = isAddForm;
            Resource = resource;
        }
        public bool IsAddForm { get; set; }
        public string FormType { get { return IsAddForm ? "AddForm" : "EditForm"; } }
        public string Resource { get; set; }
        public string GetHtmlId(string name, bool isForExistingResource, bool isDetailPartial)
        {
            return (isForExistingResource, isDetailPartial) switch
            {
                (true, true) => string.Format("see{0}DetailForExisting{1}{2}", name, Resource, FormType),
                (true, false) => string.Format("add{0}ToExisting{1}{2}", name, Resource, FormType),
                (false, true) => string.Format("see{0}DetailForNew{1}{2}", name, Resource, FormType),
                (false, false) => string.Format("add{0}ToNew{1}{2}", name, Resource, FormType)
            };
        }

    }

    public class PropertyPartialDataContainer : FormPartialDataContainer
    {
        public TabType TabType;
        public PropertyPartialDataContainer(string resource, bool isAddForm, TabType tabType)
            : base(resource, isAddForm)
        {
            TabType = tabType;
        }
        public string GeHtmlTags()
        {
            return TabType == TabType.Detail ? " disabled " : string.Empty;
        }
    }

    public class TablePartialDataContainer : FormPartialDataContainer
    {
        public TablePartialDataContainer(string resource, bool isAddForm, string[] columns)
            : base(resource, isAddForm)
        {
            Columns = columns;
        }

        public string[] Columns { get; set; }
    }

    public enum TabType
    {
        Overview,
        New,
        Detail,
        BaseSetting
    }

    #region Resource Properties
    public class BaseResourcePropertyContainer
    {
        public ResourceProperty BasicSettings { get; } = new ResourceProperty("Base settings", true);
    }
    public class ResourceProperty
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ResourceProperty(string name) : this(name, false) { }
        public ResourceProperty(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        public ResourcePropertyValues GetValueContainer(bool isAddForm)
        {
            var form = isAddForm ? "AddForm" : "EditForm";
            var name = Name.Replace(" ", "");
            return new ResourcePropertyValues
            {
                IsActive = IsActive,
                Name = Name,
                NavBtnIdAddNew = form + "NavBtnIdAddNew" + name,
                NavBtnIdCancel = form + "NavBtnIdCancel" + name,
                NavPaneIdNew = form + "NavPaneIdNew" + name,
                NavBtnIdCancelDetail = form + "NavBtnIdCancelDetail" + name,
                NavPaneIdDetail = form + "NavPaneIdDetail" + name,
                NavPaneIdOverview = form + "NavPaneIdOverview" + name,
                NavTabId = form + "NavTabId" + name,
                NavBtnIdNew = form + "NavBtnIdNew" + name

            };
        }


        public string GetPopulateMethodName()
        {
            return "Populate" + Name;
        }
        public string GetUnpopulateMethodName()
        {
            return "Unpopulate" + Name;
        }

    }

    public class ResourcePropertyValues
    {
        public string NavTabId { get; set; }
        public string NavPaneIdOverview { get; set; }
        public string NavPaneIdNew { get; set; }
        public string NavPaneIdDetail { get; set; }
        public string NavBtnIdCancel { get; set; }
        public string NavBtnIdCancelDetail { get; set; }
        public string NavBtnIdNew { get; set; }
        public string NavBtnIdAddNew { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion
}
