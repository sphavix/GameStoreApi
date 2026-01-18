using System;

namespace GameStore.Api.Dtos;
/// <summary>
/// A Dto is a contract between the client and server since it represent 
/// a shared agreement about how data will be transferred and used.
/// </summary>
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);