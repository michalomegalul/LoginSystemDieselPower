namespace BlazorApp1.Userside;

public class User
{
    public string Desktop_Name { get; set; } = UserManagment.GetPCName();
    public string Motherboard_ID { get; set; } = UserManagment.GetMotherboardId();


}