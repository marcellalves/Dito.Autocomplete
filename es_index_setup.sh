curl -H 'Content-Type: application/json' \
       -X PUT http://localhost:9200/autocomplete_index

curl -H 'Content-Type: application/json' \
        -X PUT http://localhost:9200/autocomplete_index/_mapping \
        -d \
       "{ \
            \"properties\": { \
               \"event\": { \
                  \"type\": \"completion\", \
                  \"analyzer\": \"simple\", \
                  \"preserve_separators\": true, \
                  \"preserve_position_increments\": true, \
                  \"max_input_length\": 50 \
               }, \
               \"timeStamp\": { \
                  \"type\": \"text\" \
               } \
            }\
       }"