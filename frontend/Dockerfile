# Użyj oficjalnego obrazu Node.js jako bazowego
FROM node:22.12.0 AS build

# Ustaw katalog roboczy wewnątrz kontenera
WORKDIR /app

# Skopiuj plik package.json i package-lock.json
COPY package.json .

# Zainstaluj zależności
RUN npm install

# Skopiuj pozostałe pliki aplikacji
COPY . .

EXPOSE 3000

CMD ["npm", "run", "start"]