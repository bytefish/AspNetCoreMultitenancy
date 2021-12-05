# Providing Multitenancy with ASP.NET Core and PostgreSQL Row Level Security #

## Project ##

This project is an example for Multi Tenancy with ASP.NET Core and PostgreSQL Row Level Security:

* https://www.bytefish.de/blog/aspnetcore_multitenancy.html

### Example ###


We start with inserting customers to the database of Tenant ``Tenant 1`` (``33F3857A-D8D7-449E-B71F-B5B960A6D89A``):

```
> curl -H "X-TenantName: 33F3857A-D8D7-449E-B71F-B5B960A6D89A" -H "Content-Type: application/json" -X POST -d "{\"firstName\" : \"Philipp\", \"lastName\" : \"Wagner\"}"  http://localhost:5000/api/customer

{"id":1,"firstName":"Philipp","lastName":"Wagner"}

> curl -H "X-TenantName: 33F3857A-D8D7-449E-B71F-B5B960A6D89A" -H "Content-Type: application/json" -X POST -d "{\"firstName\" : \"Max\", \"lastName\" : \"Mustermann\"}"  http://localhost:5000/api/customer

{"id":2,"firstName":"Max","lastName":"Mustermann"}
```

Getting a list of all customers for ``Tenant 1`` will now return two customers:

```
> curl -H "X-TenantName: 33F3857A-D8D7-449E-B71F-B5B960A6D89A" -H "Content-Type: application/json" -X GET http://localhost:5000/api/customer

[{"id":1,"firstName":"Philipp","lastName":"Wagner"},{"id":2,"firstName":"Max","lastName":"Mustermann"}]
```

While requesting a list of all customers for ``Tenant 2`` (``7344384A-A2F4-4FC4-A382-315FCB421A72``) returns an empty list:

```
> curl -H "X-TenantName: 7344384A-A2F4-4FC4-A382-315FCB421A72" -H "Content-Type: application/json" -X GET http://localhost:5000/api/customer

[]
```

We can now insert a customer for ``Tenant 2``:

```
> curl -H "X-TenantName: 7344384A-A2F4-4FC4-A382-315FCB421A72" -H "Content-Type: application/json" -X POST -d "{\"firstName\" : \"Hans\", \"lastName\" : \"Wurst\"}"  http://localhost:5000/api/customer

{"id":3,"firstName":"Hans","lastName":"Wurst"}
```

Querying the database with ``Tenant 1`` still returns the two customers:

```
> curl -H "X-TenantName: 33F3857A-D8D7-449E-B71F-B5B960A6D89A" -H "Content-Type: application/json" -X GET http://localhost:5000/api/customer

[{"id":1,"firstName":"Philipp","lastName":"Wagner"},{"id":2,"firstName":"Max","lastName":"Mustermann"}]
```

Querying with ``Tenant 2`` will now return the inserted customer:

```
> curl -H "X-TenantName: 7344384A-A2F4-4FC4-A382-315FCB421A72" -H "Content-Type: application/json" -X GET http://localhost:5000/api/customer

[{"id":3,"firstName":"Hans","lastName":"Wurst"}]
```

Works!