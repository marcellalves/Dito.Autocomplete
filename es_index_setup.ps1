$UrlIndex = "http://localhost:9200/autocomplete_index"
$BodyIndex = @'
{
    "settings": {
        "number_of_shards": 1,
        "analysis": {
            "filter": {
                "autocomplete_filter": {
                    "@type": "edge_ngram",
                    "min_gram": 2,
                    "max_gram": 20
                }
            },
            "analyzer": {
                "autocomplete": {
                    "@type": "custom",
                    "tokenizer": "standard\",
                    "filter": [
                        "lowercase",
                        "autocomplete_filter"
                    ] 
                }
            }
        }
    }
}
'@

Invoke-RestMethod -Uri $UrlIndex -Body $BodyIndex -Method Put -ContentType 'application/json'

$UrlMapping = "http://localhost:9200/autocomplete_index/_mapping"
$BodyMapping = @'
{
    "properties": {
        "Event": {
            "@type": "text",
            "analyzer": "autocomplete"
        },
        "TimeStamp": {
            "@type": "text"
        }
    }
}
'@

Invoke-RestMethod -Uri $UrlMapping -Body $BodyMapping -Method Put -ContentType 'application/json'