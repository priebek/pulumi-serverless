workspace "Name" "Description" {

    !identifiers hierarchical

    model {
        u = person "User"
        ss = softwareSystem "NPU System" {
            group "Frontend" {
                web = container "Web Application"
                mobile = container "Mobile Application" {
                    tags "Mobile"
                }
            }
            group "Backend" {
                af_http = container "API" "Http triggered functions. Handle frontend users requests. CRUD submission, search and score" "Azure Functions"
                af_event = container "Background Service" "Queue triggered functions" "Azure Functions"
                db = container "Database" "Stores user submissions and scores" "Azure SQL Database" "Database"
                ds = container "Data Storage" "Stores user submission images" "Azure Storage" "Database"
                adb2c = container "User Authentication" "Customer identity and access management" "Azure Active Directory B2C"
                aqs = container "Message queue system" "Queue incoming submissions and scores" "Azure Queue Storage"
                akv = container "Key Vault" "Store secrets and keys" "Azure Key Vault"
            }
        }

        u -> ss.web "Uses"
        u -> ss.mobile "Uses"
        
        ss.web -> ss.af_http "Uses"
        ss.mobile -> ss.af_http "Uses"
        
        ss.af_http -> ss.aqs "Write"
        ss.af_http -> ss.adb2c "Uses"
        ss.af_http -> ss.db "Read"
        ss.af_http -> ss.ds "Read/Write"
        ss.af_http -> ss.akv "Read"
        
        ss.af_event -> ss.aqs "Read"
        ss.af_event -> ss.db "Write"
        ss.af_event -> ss.ds "Write"
    }

    views {
        systemContext ss "Context" {
            include *
            autolayout lr
        }

        container ss "Container" {
            include *
            autolayout lr
        }

        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #199b65
                shape person
            }
            element "Software System" {
                background #1eba79
            }
            element "Container" {
                background #23d98d
            }
            element "Database" {
                shape cylinder
            }
        }
    }

    configuration {
        scope softwaresystem
    }

}