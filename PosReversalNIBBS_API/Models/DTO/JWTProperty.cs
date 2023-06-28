namespace PosReversalNIBBS_API.Models.DTO
{
    public class JWTProperty
    {
     
            public bool fresh { get; set; }
            public string iat { get; set; }
            public string jti { get; set; }
            public string type { get; set; }
            public string sub { get; set; }
            public string nbf { get; set; }
            public string exp { get; set; }
 

    }
}
