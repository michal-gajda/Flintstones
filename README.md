# The Flintstones

- Install [Seq](https://datalust.co/download)
- Install [OpenTelemetry Collector](https://github.com/open-telemetry/opentelemetry-collector-releases/releases/tag/v0.159.0)

## OpenTelemetry Collector

### OTLP

```yaml
receivers:
  otlp:
    protocols:
      grpc:
        endpoint: 0.0.0.0:4317
      http:
        endpoint: 0.0.0.0:4318

exporters:
  otlphttp/seq:
    endpoint: http://localhost:5341/ingest/otlp
    tls:
      insecure: true

service:
  pipelines:
    logs:
      receivers: [otlp]
      exporters: [otlphttp/seq]

    metrics:
      receivers: [otlp]
      exporters: [otlphttp/seq]

    traces:
      receivers: [otlp]
      exporters: [otlphttp/seq]
```

### Standard & Contrib

```yaml
extensions:
  health_check:
    endpoint: 0.0.0.0:13133

receivers:
  otlp:
    protocols:
      grpc:
        endpoint: 0.0.0.0:4317
      http:
        endpoint: 0.0.0.0:4318

processors:
  memory_limiter:
    check_interval: 1s
    limit_percentage: 80
    spike_limit_percentage: 20

  filter:
    error_mode: ignore
    logs:
      log_record:
        - 'body == "DEBUG"'
        - 'attributes["http.target"] == "/health"'

  attributes:
    actions:
      - key: env
        value: production
        action: insert

  batch:
    send_batch_size: 100
    timeout: 1s
    send_batch_max_size: 500

exporters:
  otlphttp/seq:
    endpoint: http://localhost:5341/ingest/otlp
    tls:
      insecure: true

  otlphttp/phoenix:
    endpoint: http://localhost:6006
    sending_queue:
      enabled: false
    retry_on_failure:
      enabled: false
    tls:
      insecure: true

  otlphttp/prometheus:
    endpoint: http://localhost:9090/api/v1/otlp
    sending_queue:
      enabled: false
    retry_on_failure:
      enabled: false
    tls:
      insecure: true

  otlphttp/elasticsearch:
    endpoint: "http://localhost:8205"
    sending_queue:
      enabled: false
    retry_on_failure:
      enabled: false
    tls:
      insecure: true

service:
  extensions: [health_check]
  pipelines:
    logs:
      receivers: [otlp]
      processors: [memory_limiter, filter, attributes, batch]
      exporters: [otlphttp/seq, otlphttp/phoenix, otlphttp/elasticsearch]

    metrics:
      receivers: [otlp]
      processors: [memory_limiter, batch]
      exporters: [otlphttp/seq, otlphttp/phoenix, otlphttp/prometheus, otlphttp/elasticsearch]

    traces:
      receivers: [otlp]
      processors: [memory_limiter, batch]
      exporters: [otlphttp/seq, otlphttp/phoenix, otlphttp/elasticsearch]
```

## Fred

## Wilma