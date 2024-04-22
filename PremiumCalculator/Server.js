const http = require('http');
const path = require('path');
const fs = require('fs');

const PORT = 3000;
const HTML_FILE_PATH = path.join(__dirname, 'Index.html');

const server = http.createServer((req, res) => {
    if (req.url === '/') {
        fs.readFile(HTML_FILE_PATH, (err, data) => {
            if (err) {
                res.writeHead(500);
                return res.end('Error al leer el archivo HTML');
            }
            res.writeHead(200, { 'Content-Type': 'text/html' });
            res.write(data);
            res.end();
        });
    } else {
        res.writeHead(404);
        res.end('Página no encontrada');
    }
});

server.listen(PORT, () => {
    console.log(`Servidor en ejecución en http://localhost:${PORT}`);
});
