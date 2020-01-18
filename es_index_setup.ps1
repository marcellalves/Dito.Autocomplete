$UrlIndex = "http://localhost:9200/autocomplete_index"

Invoke-RestMethod -Uri $UrlIndex -Method Put -ContentType 'application/json'

$UrlMapping = "http://localhost:9200/autocomplete_index/_mapping"
$BodyMapping = @'
{
    "properties": {
       "event": {
          "type": "completion",
          "analyzer": "simple",
          "preserve_separators": true,
          "preserve_position_increments": true,
          "max_input_length": 50
       },
       "timeStamp": {
          "type": "text"
       }
    }
}
'@

Invoke-RestMethod -Uri $UrlMapping -Body $BodyMapping -Method Put -ContentType 'application/json'