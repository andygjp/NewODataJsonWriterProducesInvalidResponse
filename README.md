# NewODataJsonWriterProducesInvalidResponse

A basic sample to demonstrate https://github.com/OData/AspNetCoreOData/issues/673

# Request

```text
POST http://localhost:5299/default/$batch
Content-Type: application/json
```

```json
{
  "requests": [
    {
      "id": "1",
      "method": "GET",
      "url": "Customers"
    },
    {
      "id": "2",
      "method": "GET",
      "url": "Customers"
    },
    {
      "id": "3",
      "method": "GET",
      "url": "Customers"
    }
  ]
}
```

# Invalid JSON Response

```text
HTTP/1.1 200 OK
Content-Type: application/json
Date: Wed, 24 Aug 2022 19:17:59 GMT
Server: Kestrel
Transfer-Encoding: chunked
OData-Version: 4.0
```

```json
{
  "responses": [
    {
      "id": "1",
      "status": 200,
      "headers": {
        "content-type": "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8",
        "odata-version": "4.0"
      },
      "body":
    },
    {
      "id": "2",
      "status": 200,
      "headers": {
        "content-type": "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8",
        "odata-version": "4.0"
      },
      "body": {
        "@odata.context": "http://localhost:5299/default/$metadata#Customers",
        "value": [
          {
            "ID": 1,
            "Name": "John Smith"
          }
        ]
      }
    },
    {
      "id": "3",
      "status": 200,
      "headers": {
        "content-type": "application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8",
        "odata-version": "4.0"
      },
      "body": {
        "@odata.context": "http://localhost:5299/default/$metadata#Customers",
        "value": [
          {
            "ID": 1,
            "Name": "John Smith"
          }
        ]
      }
    }
  ]
}{
  "@odata.context": "http://localhost:5299/default/$metadata#Customers",
  "value": [
    {
      "ID": 1,
      "Name": "John Smith"
    }
  ]
}
```
