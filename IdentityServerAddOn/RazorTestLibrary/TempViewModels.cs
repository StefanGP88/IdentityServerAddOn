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
        public PartialDataContainer(string resource, string property, bool isForExistingResource, bool isDetailPartial)
        {
            Resource = resource;
            IsForExistingResource = isForExistingResource;
            IsDetailPartial = isDetailPartial;
            Property = property;
        }
        public string Resource { get; set; }
        public string Property { get; set; }
        public bool IsForExistingResource { get; set; }
        public bool IsDetailPartial { get; set; }
        public string GetHtmlId(string name)
        {
            return (IsForExistingResource, IsDetailPartial) switch
            {
                (true, true) => string.Format("see{0}DetailForExisting{1}", name, Resource),
                (true, false) => string.Format("add{0}ToExisting{1}", name, Resource),
                (false, true) => string.Format("see{0}DetailForNew{1}", name, Resource),
                (false, false) => string.Format("add{0}ToNew{1}", name, Resource)
            };
        }

        public string GeHtmlTags()
        {
            return IsDetailPartial ? " disabled " : string.Empty;
        }

        public string GetPopulateMethodName(string name)
        {
            if (IsForExistingResource) return string.Format("Populate{0}ForExisitng(element)", name);
            return string.Format("Populate{0}ForNew(element)", name);
        }
        public string GetUnpopulateMethodName(string name)
        {
            if (IsForExistingResource) return string.Format("Populate{0}ForExisitng(element)", name);
            return string.Format("Unopulate{0}ForNew(element)", name);
        }
    }

    public class TablePartialDataContainer : PartialDataContainer
    {
        public TablePartialDataContainer(string resource, string property, bool isForExistingResource, string[] columns)
            : base(resource, property, isForExistingResource, false)
        {
            Columns = columns;
        }

        public string[] Columns { get; set; }

        public string GetAddButtonId()
        {
            if (IsForExistingResource) return string.Format("add{0}ToExisitng{1}Btn", Property, Resource);
            return string.Format("add{0}ToNew{1}Btn", Property, Resource);
        }
    }
}
