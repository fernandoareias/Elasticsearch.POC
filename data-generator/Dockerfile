# Use uma imagem base do Python
FROM python:3.11-slim

# Crie e defina o diretório de trabalho
WORKDIR /app

# Copie o script Python e os arquivos de configuração
COPY generate_data.py .

# Instale as dependências do Python
RUN pip install confluent_kafka

# Defina a variável de ambiente para o Kafka
ENV KAFKA_BOOTSTRAP_SERVERS=kafka:9092
ENV KAFKA_TOPIC=request-processed-event

# Execute o script Python
CMD ["python", "generate_data.py"]
