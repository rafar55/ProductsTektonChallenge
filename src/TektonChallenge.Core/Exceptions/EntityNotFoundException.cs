namespace TektonChallenge.Core.Exceptions;

public class EntityNotFoundException(string id) : Exception($"Id {id} not found on the database");