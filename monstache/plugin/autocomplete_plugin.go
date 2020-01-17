package main

import (
	"log"
	"os"

	"github.com/rwynn/monstache/monstachemap"
)

func Process(input *monstachemap.ProcessPluginInput) (err error) {
	var infoLog = log.New(os.Stdout, "INFO ", log.Flags())
	infoLog.Printf("Iniciou a execução do método Process")

	indexName := "autocomplete_index"

	index := `{
		"settings" : {
			"number_of_shards": 1,
			"analysis": {
				"filter": {
					"autocomplete_filter": {
						"type": "edge_ngram",
                        "min_gram": 2,
                        "max_gram": 20
					}
				},
				"analyzer": {
					"autocomplete": {
						"type": "custom",
                        "tokenizer": "standard",
                        "filter": [
                            "lowercase",
                            "autocomplete_filter"
                        ]
					}
				}
			}
	}`

	input.ElasticClient.CreateIndex(indexName).BodyString(index)

	mapping := `{
		"properties": {
			"Event": {
				"type": "text",
				"analyzer": "autocomplete"
			},
			"TimeStamp": {
				"type": "text"
			}
		}
	}`

	input.ElasticClient.PutMapping().Index(indexName).BodyString(mapping)
	return
}
