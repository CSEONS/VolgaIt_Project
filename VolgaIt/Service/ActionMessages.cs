namespace VolgaIt.Service
{
    public static class ActionMessages
    {
        public static string UsernameTaken(string username) => $"A username \"{username}\" is already registered.";
        public static string UserNotFound() => $"User not found.";
        public static string UserCreated() => $"User created.";
        public static string TransportCreated() => $"Transport created.";
        public static string UserDeleted() => $"User deleted.";
        public static string TransportDeleted() => $"Transport deleted.";
        public static string TransportUpdated() => $"Transport updated.";
        public static string YouNotOwner() => $"You are not the owner of this transport.";
        public static string TransportNotFound() => $"Transport not found.";
        public static string SignInFailed() => $"Failed to login. You may have entered an incorrect login or password.";
        public static string InvalidCredintials() => "Invalid Credentials.";
        public static string SignOut() => "Sign out.";
        public static string UserUpdated() => "User updated.";
        public static string ParametrsMustGreaterZero(params string[] args) => $"The {string.Join(", ", args)} parametrs must be greater than zero.";
        public static string InvalidQueryParametrs() => "The \"start\" and \"count\" parameters must be greater than zero.";
        public static string UnknownTransportType() => "Unknown transport type";
        public static string RentNotFound() => "Rent not found.";
        public static string AccesDined() => "Access denied.";
        public static string UnknownRentType() => "Unknown rent type";
        public static string TransportRented() => "Transport rented";
        public static string Hesoyam() => "+250 000";
        public static string RentUpdated() => "Rent updated.";
        public static string RentDeleted() => "Rent deleted";
    }
}
