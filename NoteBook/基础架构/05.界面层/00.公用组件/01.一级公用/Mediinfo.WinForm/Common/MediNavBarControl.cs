using DevExpress.XtraNavBar;

using System.ComponentModel;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public class MediNavBarControl : NavBarControl
    {
        protected override NavGroupCollection CreateGroupCollection()
        {
            return new MediNavGroupCollection(this);
        }

        protected override NavItemCollection CreateItemCollection()
        {
            return new MediNavItemCollection(this);
        }
    }
    
    public class MediNavGroupCollection : NavGroupCollection
    {
        public MediNavGroupCollection(MediNavBarControl mediNavBarControl) : base(mediNavBarControl)
        {

        }
        
        protected override ICollectionItem CreateItem()
        {
            return new MediNavBarGroup();
        }
    }
    
    public class MediNavBarGroup : NavBarGroup
    {

    }

    public class MediNavItemCollection : NavItemCollection
    {
        public MediNavItemCollection(MediNavBarControl mediNavBarControl) : base(mediNavBarControl)
        {

        }
        
        protected override ICollectionItem CreateItem()
        {
            return new MediNavBarItem();
        }
    }
    
    public class MediNavBarItem : NavBarItem
    {

    }
}
