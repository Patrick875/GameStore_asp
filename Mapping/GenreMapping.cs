using System;
using GameStore_API.Dtos;
using GameStore_API.Models;

namespace GameStore_API.Mapping;

public  static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre){
    return new GenreDto(genre.id,genre.Name);
    }
}
