namespace NetCoreStartProject.Enums
{
    public enum ServiceClass
    {
        WeddingHall = 1,  // rent
        WeddingPlanner = 2,  // rent
        WeddingCar = 3,  // rent
        Photographers = 4, // rent
        PhotoSetion = 5, // rent
        MakeupArtist = 6, // rent
        WeddingDress = 7,  // rent
        FlowerBouquet = 8,  // buy
        WeddingInvitations = 9  // buy
    }
    public enum ServiceType
    {
        Rent = 0,
        Buy = 1,
        RentOrBuy
    }
}
