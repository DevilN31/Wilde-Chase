using System.Collections.Generic;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Extensions
{
  public static class ArrayExtensions
  {
    public static T GetRandom<T>( this List<T> list )
    {
      if ( list.Count == 0 ) return default( T );
			
      return list[Random.Range( 0, list.Count )];
    }
  }
}