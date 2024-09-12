import uuid
import random
import datetime
import json
import os
import time 
from confluent_kafka import Producer

def generate_random_uuid():
    return str(uuid.uuid4())

def generate_random_cpf():
    def calculate_check_digits(cpf):
        weights = [10, 9, 8, 7, 6, 5, 4, 3, 2]
        sum_digits = sum(int(cpf[i]) * weights[i] for i in range(9))
        remainder = (sum_digits * 10) % 11
        return 0 if remainder == 10 else remainder

    cpf = [str(random.randint(0, 9)) for _ in range(9)]
    first_digit = calculate_check_digits(cpf)
    cpf.append(str(first_digit))
    cpf.append(str(calculate_check_digits(cpf)))
    return ''.join(cpf)

def generate_random_ip():
    return f"{random.randint(0, 255)}.{random.randint(0, 255)}.{random.randint(0, 255)}.{random.randint(0, 255)}"

def generate_random_coordinates():
    return round(random.uniform(-90, 90), 1), round(random.uniform(-180, 180), 1)

def generate_data():
    return {
        "id": generate_random_uuid(),
        "event": "REQUEST-PROCESSED",
        "data": {
            "id": generate_random_uuid(),
            "deviceId": generate_random_uuid(),
            "document": generate_random_cpf(),
            "ip": generate_random_ip(),
            "platform": random.choice(["Android", "iOS"]),
            "lat": generate_random_coordinates()[0],
            "long": generate_random_coordinates()[1]
        },
        "createdAt": datetime.datetime.utcnow().isoformat() + 'Z'
    }

def delivery_callback(err, msg):
    if err:
        print(f'%% Message failed delivery: {err}')
    else:
        print(f'%% Message delivered to {msg.topic()} [{msg.partition()}]')

def main():
    conf = {
        'bootstrap.servers': os.getenv('KAFKA_BOOTSTRAP_SERVERS'),
        'client.id': 'data-generator'
    }

    p = Producer(conf)

    topic = os.getenv('KAFKA_TOPIC', 'request-processed-event')
    while True:
        data = generate_data()
        p.produce(topic, json.dumps(data), callback=delivery_callback)
        p.poll(0)  # Processa mensagens pendentes
        time.sleep(5)

    p.flush()

if __name__ == "__main__":
    main()
