﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class Animal
    {
        public string species;
        public string habitat_level;
        public float? min_age;
        public float? max_age;
        public float? min_weight;
        public float? max_weight;
        public float? min_height;
        public float? max_height;
    }

    public class AnimalData
    {
        public string species;
        public string name;
        public string description;
        public string habitat_level;
        public float min_size;
        public float max_size;
        public float min_age;
        public float max_age;
        public float min_weight;
        public float max_weight;
        public string colorkey_map_file;
    }

    public class AnimalsController : ApiController
    {
        [HttpGet]
        public IEnumerable<AnimalData> Get([FromBody]Animal animal)
        //public string Get([FromBody]Animal animal)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM Animals");

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Tamuyal"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query.ToString(), connection);
            SqlDataReader reader = command.ExecuteReader();

            string result = connection.State.ToString();

            List<AnimalData> animals = new List<AnimalData>();
            while (reader.Read())
            {
                AnimalData data = new AnimalData();
                data.species = reader["species"].ToString();
                data.name = reader["animal_name"].ToString();
                data.description = reader["description"].ToString();
                data.habitat_level = reader["habitat_level"].ToString();
                data.min_size = float.Parse(reader["min_height"].ToString());
                data.max_size = float.Parse(reader["max_height"].ToString());
                data.min_age = float.Parse(reader["min_age"].ToString());
                data.max_age = float.Parse(reader["max_age"].ToString());
                data.min_weight = float.Parse(reader["min_weight"].ToString());
                data.max_weight = float.Parse(reader["max_weight"].ToString());
                data.colorkey_map_file = reader["colorkey_map_file"].ToString();
                animals.Add(data);
            }

            connection.Close();
            return animals;



            //StringBuilder query = new StringBuilder();
            //query.Append("SELECT * FROM Animals");
            //return animal.species;
            /*if (animal == null)
            {
                 var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Tamuyal"].ConnectionString;
                 SqlConnection connection = new SqlConnection(connectionString);
                 connection.Open();
                 SqlCommand command = new SqlCommand(query.ToString(), connection);
                 SqlDataReader reader = command.ExecuteReader();

                 string result = connection.State.ToString();

                List<AnimalData> animals = new List<AnimalData>();
                 while(reader.Read())
                 {
                    AnimalData data = new AnimalData();
                    data.species = reader["species"].ToString();
                    data.name = reader["animal_name"].ToString();
                    data.description = reader["description"].ToString();
                    data.habitat_level = reader["habitat_level"].ToString();
                    data.min_size = float.Parse(reader["min_height"].ToString());
                    data.max_size = float.Parse(reader["max_height"].ToString());
                    data.min_age = float.Parse(reader["min_age"].ToString());
                    data.max_age = float.Parse(reader["max_age"].ToString());
                    data.min_weight = float.Parse(reader["min_weight"].ToString());
                    data.max_weight = float.Parse(reader["max_weight"].ToString());
                    data.colorkey_map_file = reader["colorkey_map_file"].ToString();
                    animals.Add(data);
                 }
  
                connection.Close();
                return animals;
            }*/









            /*  query.Append(" WHERE ");
              bool appendAnd = false;

              StringBuilder species = new StringBuilder("");
              StringBuilder habitatLevel = new StringBuilder("");
              StringBuilder age = new StringBuilder("");
              StringBuilder weight = new StringBuilder("");
              StringBuilder height = new StringBuilder("");

              if (animal.Species != null)
              {
                  species.Append("species = ");
                  species.Append(animal.Species);
                  appendAnd = true;
              }
              if (animal.HabitatLevel != null)
              {
                  if (appendAnd)
                  {
                      habitatLevel.Append(" and ");
                  }
                  habitatLevel.Append("habitat_level = ");
                  habitatLevel.Append(animal.HabitatLevel);
                  appendAnd = true;
              }

              if (animal.MaxAge != null && animal.MinAge != null)
              {
                  if (appendAnd)
                  {
                      age.Append(" and ");
                  }
                  age.Append("age > ");
                  age.Append(animal.MinAge);
                  age.Append(" and ");
                  age.Append("age < ");
                  age.Append(animal.MaxAge);
                  appendAnd = true;
              }
              else if (animal.MaxAge != null)
              {
                  if (appendAnd)
                  {
                      age.Append(" and ");
                  }
                  age.Append("age < ");
                  age.Append(animal.MaxAge);
                  appendAnd = true;
              }
              else if (animal.MinAge != null)
              {
                  if (appendAnd)
                  {
                      age.Append(" and ");
                  }
                  age.Append("age > ");
                  age.Append(animal.MinAge);
                  appendAnd = true;
              }

              if (animal.MaxWeight != null && animal.MinWeight != null)
              {
                  if (appendAnd)
                  {
                      weight.Append(" and ");
                  }
                  weight.Append("age > ");
                  weight.Append(animal.MinWeight);
                  weight.Append(" and ");
                  weight.Append("age < ");
                  weight.Append(animal.MaxWeight);
                  appendAnd = true;
              }
              else if (animal.MaxWeight != null)
              {
                  if (appendAnd)
                  {
                      weight.Append(" and ");
                  }
                  weight.Append("age < ");
                  weight.Append(animal.MaxWeight);
                  appendAnd = true;
              }
              else if (animal.MinWeight != null)
              {
                  if (appendAnd)
                  {
                      weight.Append(" and ");
                  }
                  weight.Append("age > ");
                  weight.Append(animal.MinWeight);
                  appendAnd = true;
              }

              if (animal.MaxHeight != null && animal.MinHeight != null)
              {
                  if (appendAnd)
                  {
                      height.Append(" and ");
                  }
                  height.Append("age > ");
                  height.Append(animal.MinHeight);
                  height.Append(" and ");
                  height.Append("age < ");
                  height.Append(animal.MaxHeight);
              }
              else if (animal.MaxHeight != null)
              {
                  if (appendAnd)
                  {
                      height.Append(" and ");
                  }
                  height.Append("age < ");
                  height.Append(animal.MaxHeight);
              }
              else if (animal.MinHeight != null)
              {
                  if (appendAnd)
                  {
                      height.Append(" and ");
                  }
                  height.Append("age > ");
                  height.Append(animal.MinHeight);
              }

              StringBuilder finalQuery = new StringBuilder();
              finalQuery.Append(query);
              finalQuery.Append(species);
              finalQuery.Append(habitatLevel);
              finalQuery.Append(age);
              finalQuery.Append(weight);
              finalQuery.Append(height);

              AnimalData lala = new AnimalData();
              lala.species = finalQuery.ToString();
              List<AnimalData> lili = new List<AnimalData>();
              lili.Add(lala);
              //return Enumerable.Empty<AnimalData>();
              return lili;
              */
        }

        [HttpPut]
        public List<AnimalData> Put([FromBody] Animal animal)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM Animals");
            query.Append(" WHERE ");
            bool appendAnd = false;

            StringBuilder species = new StringBuilder("");
            StringBuilder habitatLevel = new StringBuilder("");
            StringBuilder age = new StringBuilder("");
            StringBuilder weight = new StringBuilder("");
            StringBuilder height = new StringBuilder("");

            if (animal.species != null)
            {
                species.Append("species = '");
                species.Append(animal.species);
                species.Append("'");
                appendAnd = true;
            }
            if (animal.habitat_level != null)
            {
                if (appendAnd)
                {
                    habitatLevel.Append(" and ");
                }
                habitatLevel.Append("habitat_level = '");
                habitatLevel.Append(animal.habitat_level);
                habitatLevel.Append("'");
                appendAnd = true;
            }

            if (animal.max_age != null && animal.min_age != null)
            {
                if (appendAnd)
                {
                    age.Append(" and ");
                }
                age.Append("age > ");
                age.Append(animal.min_age);
                age.Append(" and ");
                age.Append("age < ");
                age.Append(animal.max_age);
                appendAnd = true;
            }
            else if (animal.max_age != null)
            {
                if (appendAnd)
                {
                    age.Append(" and ");
                }
                age.Append("age < ");
                age.Append(animal.max_age);
                appendAnd = true;
            }
            else if (animal.min_age != null)
            {
                if (appendAnd)
                {
                    age.Append(" and ");
                }
                age.Append("age > ");
                age.Append(animal.min_age);
                appendAnd = true;
            }

            if (animal.max_weight != null && animal.min_weight != null)
            {
                if (appendAnd)
                {
                    weight.Append(" and ");
                }
                weight.Append("weight > ");
                weight.Append(animal.min_weight);
                weight.Append(" and ");
                weight.Append("weight < ");
                weight.Append(animal.max_weight);
                appendAnd = true;
            }
            else if (animal.max_weight != null)
            {
                if (appendAnd)
                {
                    weight.Append(" and ");
                }
                weight.Append("weight < ");
                weight.Append(animal.max_weight);
                appendAnd = true;
            }
            else if (animal.min_weight != null)
            {
                if (appendAnd)
                {
                    weight.Append(" and ");
                }
                weight.Append("weight > ");
                weight.Append(animal.min_weight);
                appendAnd = true;
            }

            if (animal.max_height != null && animal.min_height != null)
            {
                if (appendAnd)
                {
                    height.Append(" and ");
                }
                height.Append("height > ");
                height.Append(animal.min_height);
                height.Append(" and ");
                height.Append("height < ");
                height.Append(animal.max_height);
            }
            else if (animal.max_height != null)
            {
                if (appendAnd)
                {
                    height.Append(" and ");
                }
                height.Append("height < ");
                height.Append(animal.max_height);
            }
            else if (animal.min_height != null)
            {
                if (appendAnd)
                {
                    height.Append(" and ");
                }
                height.Append("height > ");
                height.Append(animal.min_height);
            }

            StringBuilder finalQuery = new StringBuilder();
            finalQuery.Append(query);
            finalQuery.Append(species);
            finalQuery.Append(habitatLevel);
            finalQuery.Append(age);
            finalQuery.Append(weight);
            finalQuery.Append(height);

            AnimalData lala = new AnimalData();
            lala.species = finalQuery.ToString();
            List<AnimalData> lili = new List<AnimalData>();
            lili.Add(lala);
            //return Enumerable.Empty<AnimalData>();
            return lili;
        }
    }
}
