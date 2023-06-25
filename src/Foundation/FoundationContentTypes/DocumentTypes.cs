namespace Microservices.Foundation.Infrastructure
{
    public static class DocumentTypes
    {
        public static string Preference = "clientSettings";
        public static string PartnerUser = "PartnerUser";
        public static string Customer = "Customer";
        public static string Order = "Order";
        public static string POS = "POS";
        public static string PopularProducts = "PopularProducts";
        public static string Kitchen = "Kitchen";
        public static string GuideActionConfiguration = "GuideActionConfiguration";
        public static string Delivery = "Delivery";
        public static string OfferType = "OfferType";
        public static string OrderReport = "OrderReport";
        public static string CustomersIdentity = "CustomersIdentity"; 
        public static string Invitation = "Invitation";
        public static string CustomersInvitation = "CustomersInvitation";        
        public static string PartnerBusiness = "PartnerBusiness";   
        public static string Product = "Product";
        public static string FieldConfigurations = "FieldConfigurations";
        public static string ProductGroup = "ProductGroup";
        public static string Menu = "Menu";
        public static string PriceCatalog = "PriceCatalog";
        public static string State = "State";
        public static string Country = "Country";
        public static string Address = "Address";
        public static string Wallets = "Wallets";
        public static string Recharge = "Recharge";
        public static string Payment = "Payment";
        public static string Availability = "Availability";
        public static string MenuGroup = "MenuGroup";
        public static string Ingredient = "Ingredient";
        public static string Slot = "Slot";
        public static string Shop = "Shop";
        public static string AppTagOption = "AppTagOption";
        public static string Restaurant = "Restaurant";
        public static string Outlet = "Outlet";
        public static string RestaurantSection = "RestaurantSection";
        public static string Video = "Video";
        public static string TableStatusOption = "TableStatusOption";
        public static string AaryaImage = "AaryaImage";
        public static string Price = "Price";
        public static string Dictionary = "Dictionary Entry";
        public static string Lists = "Lists";
        public static string CustomerPreferencesItem = "CustomerPreferences";
        public static string GuideAction = "GuideAction";
        public static string Roles = "Roles";
        public static string AppModule = "AppModule";
        public static string BusinessEntity = "BusinessEntity";
        public static string Marketing = "__marketing";
        public static string FoodLabel = "FoodLabel";
        public static string Currency = "Currency";
        public static string PaymentType = "PaymentType";
        public static string FulfillmentOption = "FulfillmentOption";
        public static string Unit = "Unit";
        public static string FoodState = "FoodState";
        public static string Flavour = "Flavour";
        public static string ServicePrimaryType = "ServicePrimaryType";
        public static string Cuisine = "Cuisine";
        public static string GuideActionType = "GuideActionType";
        public static string ActionPriority = "ActionPriority";
        public static string Salutation = "Salutation";
        public static string CustomerBasket = "CustomerBasket";
        public static string Offers = "Offers";
        public static string Coupons = "Coupons";
        public static string LineItem = "LineItem";
        public static string ProductVariantLine = "ProductVariant";
        public static string PaymentLineItem = "PaymentLineItem";
        public static string DiscountLineItem = "DiscountLineItem";
        public static string FulfillmentLineItem = "FulfillmentLineItem";
        public static string CartInstructionsLineItem = "CartInstructionsLineItem";
        public static string TaxLineItem = "TaxLineItem";
        public static string GuideConfigurationResultItem = "GuideConfigurationResult";
        public static string B2BPayment = "B2BPayment";
        public static string B2BPaymentLog = "B2BPaymentLog";
        public static string B2CPayment = "B2CPayment";
        public static string B2CPaymentLog = "B2CPaymentLog";
        public static string B2BWallet = "B2BWallet";
        public static string B2BWalletLog = "B2BWalletLog";
        public static string B2CWallet = "B2CWallet";
        public static string B2CWalletLog = "B2CWalletLog";
    }

    public static class AppModules
    {
        public static string Slots = "Slots";
        public static string Menus = "Menus";
        public static string Products = "Products";
        public static string ProductGroups = "ProductGroups";
        public static string MenuGroups = "MenuGroups";
        public static string ProductPricings = "ProductPricings";
        public static string Availability = "Availability";
        public static string Ingredients = "Ingredients";
        public static string Promotions = "Promotions";
        public static string QR = "QR";
        public static string SocialNetwork = "SocialNetwork";
        public static string RestaurantManagement = "Restaurant Management";
        public static string RestaurantSection = "Restaurant Section";
        public static string Outlet = "Outlet";
        public static string PhotoManagement = "Photo Management";
        public static string Business = "Business";
        public static string Address = "Address";
        public static string Invitations = "Invitations";
        public static string UserSettings = "UserSettings";
        public static string Users = "Users";
        public static string Lists = "Lists";
        public static string Languages = "Languages";
        public static string MyTenants = "My Tenants";
        public static string Customers = "Customers";
        public static string Orders = "Orders";
        public static string Tables = "Tables";
    }

    public static class AppCommands
    {
        public static string RePublish = "Re-Publish";
        public static string AddTenant = "AddTenant";
        public static string AddTable = "AddTable";
        public static string AddAddress = "AddAddress";
        public static string AddIngredient = "AddIngredient";
        public static string AddMenuGroup = "AddMenuGroup";
        public static string ChooseFromOurLibrary = "ChooseFromOurLibrary";
        public static string AddProduct = "AddProduct";
        public static string AddProductGroup = "AddProductGroup";
        public static string MenuWizard = "MenuWizard";
        public static string AddMenu = "AddMenu";
        public static string AddOutlet = "AddOutlet";
        public static string AddRestaurant = "AddRestaurant";
        public static string AddRestaurantSection = "AddRestaurantSection";
        public static string AddSlot = "AddSlot";
        public static string Invite = "Invite";
        public static string PendingActions = "PendingActions";

    }
    public static class Fields
    {
        public static List<string> SharedFields = new List<string>
        {
            "featuredmediaid","height","width","cdnimageurl"
        };
        public static List<string> TranslatedFields = new List<string>
        {
            "alt"
        };
    }
    public static class OrderStatus
    {
        public static string Submitted = "Submitted";
        public static string Open = "Open";
        public static string Completed = "Completed";
        public static string OutForDelivery = "OutForDelivery";
        public static string InKitchen = "InKitchen";
        public static string Cancelled = "Cancelled";
    }
}