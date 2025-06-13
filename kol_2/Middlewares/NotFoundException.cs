namespace kol_2.Middlewares;

public class NotFoundException(string msg) : Exception(msg);