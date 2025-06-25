# App File Uploader

## OpenTelemetry
### Aspire Dashboard

```
APP_UPD_NAME=DckUpdApp
OTLP_ENDPOINT=http://aspd_aspire-dashboard:18890
OTLP_PROTOCOL=http/protobuf
```


### Seq Dashboard


```
APP_UPD_INFRA_LOG_SEQ=http://local_seq_seq
OTLP_ENDPOINT=http://local_seq_seq:5341/ingest/otlp/v1/traces
OTLP_PROTOCOL=http/protobuf
```


## Infrastructure

### File


| Type     | Value    |
| ----------------- | ------ |
| Local | OnPrem |
| Azure Blob | AzureBlob |

