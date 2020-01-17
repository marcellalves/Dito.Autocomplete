curl -H 'Content-Type: application/json' \
       -X PUT http://localhost:9200/autocomplete_index \
       -d \
      "{ \
          \"settings\": { \
              \"number_of_shards\": 1, \
              \"analysis\": { \
                  \"filter\": { \
                      \"autocomplete_filter\": { \
                          \"type\":     \"edge_ngram\", \
                          \"min_gram\": 2, \
                          \"max_gram\": 20 \
                      } \
                  }, \
                  \"analyzer\": { \
                      \"autocomplete\": { \
                          \"type\":      \"custom\", \
                          \"tokenizer\": \"standard\", \
                          \"filter\": [ \
                              \"lowercase\", \
                              \"autocomplete_filter\" \
                          ] \
                      } \
                  } \
              } \
          } \
      }"

curl -H 'Content-Type: application/json' \
        -X PUT http://localhost:9200/autocomplete_index/_mapping \
        -d \
       "{ \
		   \"properties\": { \
			   \"Event\": { \
				   \"type\":     \"text\", \
				   \"analyzer\": \"autocomplete\" \
			   }, \
			   \"TimeStamp\": { \
				   \"type\":    \"text\" \
			   } \
		   } \
       }"