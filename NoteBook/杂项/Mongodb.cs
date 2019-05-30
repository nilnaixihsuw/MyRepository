using ClassLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ServiceStack.Redis;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Collections;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 连接
             */
            MongoClient mongoClient = new MongoClient();
            //MongoClient mongoClient = new MongoClient("mongodb://192.168.101.218:27017");

            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info",
                    new BsonDocument
                    {
                      { "x", 203 },
                      { "y", 102 }
                    }
                }
            };

            /*
             * 插入
             */
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").InsertOne(document);
            //await mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").InsertOneAsync(document);
            //var documents = Enumerable.Range(0, 100).Select(i => new BsonDocument("counter", i));
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").InsertMany(documents);
            //await collection.InsertManyAsycn(documents);

            /*
             * 获取数量
             */
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Count(new BsonDocument());
            //await mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").CountAsync(new BsonDocument());
            //空的BsonDocument参数是一个过滤器

            /*
             * 查询
             */
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(new BsonDocument()).FirstOrDefault();
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(new BsonDocument()).ToList();  //返回的文档比较少量
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(new BsonDocument()).ForEachAsync(s => Console.WriteLine(s)); //返回的文档数量比预期的大
            //var cursor = mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(new BsonDocument()).ToCursor(); //使用的是同步的API
            //foreach (var document in cursor.ToEnumerable())
            //{
            //    //...
            //}

            /*
             * 创建筛选器
             */
            //var filter = Builders<BsonDocument>.Filter.Eq("count", 1);
            //筛选count等于1的文档
            //var filter = Builders<BsonDocument>.Filter.Gt("count", 50);
            // 筛选count大于50的文档
            //筛选count大于50且小于等于100的文档
            //var filterBuilder = Builders<BsonDocument>.Filter;
            //var filter = filterBuilder.Gt("count", 50) & filterBuilder.Lte("count", 100);
            //筛选count不为空，按降序排序
            //var filter = Builders<BsonDocument>.Filter.Exists("count");
            //var sort = Builders<BsonDocument>.Sort.Descending("count");
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(filter).Sort(sort).ToCursor();
            //查询结果过滤掉count项
            //var projection = Builders<BsonDocument>.Projection.Exclude("count");
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Find(new BsonDocument()).Project(projection).FirstOrDefault();

            /*
             * 更新
             */
            //var filter = Builders<BsonDocument>.Filter.Eq("type", "Database");
            //var update = Builders<BsonDocument>.Update.Set("count", 100);
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").UpdateMany(filter, update);

            /*
             * 删除
             */
            //var filter = Builders<BsonDocument>.Filter.Gte("count", 0);// Gte表示大于等于
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").DeleteMany(filter);

            /*
             * 获取所有数据集合
             */
            //using (var cursor = mongoClient.ListDatabases())
            //{
            //    foreach (var document in cursor.ToEnumerable())
            //    {
            //        //...
            //    }
            //}

            /*
             * 创建索引
             */
             //1
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Indexes.CreateOne(new BsonDocument("count", 1));
            // 2
            //var keys = Builders<BsonDocument>.IndexKeys.Ascending("count");
            //mongoClient.GetDatabase("myDatabase").GetCollection<BsonDocument>("myCollection").Indexes.CreateOne(keys);
        }
    }
}
//安装配置：https://www.jianshu.com/p/0221b85528bf
//操作：https://www.cnblogs.com/yan7/p/8603640.html