package main

import (
	"log"
	"os"

	"github.com/rwynn/monstache/monstachemap"
)

func Process(input *monstachemap.ProcessPluginInput) (err error) {
	var infoLog = log.New(os.Stdout, "INFO ", log.Flags())
	infoLog.Printf("Iniciou a execução do mérodo Process")

	mapping := `{
		"properties" : {
			"Event" : { "type" : "string" },
			"suggest" : { "type" : "completion",
							"analyzer" : "simple",
							"search_analyzer" : "simple",
							"payloads" : true
			}
		}
	}`

	input.ElasticClient.PutMapping().Index("useractivitydb.useractivities").BodyString(mapping)
	return
}
