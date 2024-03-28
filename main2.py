import os
import pyodbc

# Detalles de conexión a la base de datos
server = os.getenv('DB_SERVER')
database = os.getenv('DB_NAME')
username = os.getenv('DB_USER')
password = os.getenv('DB_PASSWORD')

connection_string = f"Driver={{ODBC Driver 17 for SQL Server}};Server=tcp:{server},1433;Database={database};Uid={username};Pwd={password};Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;"

# Estructura de proyecto
entities_directory = "FugasDetectionSystem.Domain/Entities/"
repositories_directory = "FugasDetectionSystem.Infrastructure/Repositories/"

# Asegúrate de que los directorios existen
os.makedirs(entities_directory, exist_ok=True)
os.makedirs(repositories_directory, exist_ok=True)

def get_table_columns(table_name):
    with pyodbc.connect(connection_string) as conn:
        with conn.cursor() as cursor:
            cursor.execute(f"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table_name}'")
            columns = cursor.fetchall()
    return columns

def generate_entity_class(table_name, columns):
    class_name = table_name  # Asumiendo que el nombre de tabla coincide con la clase
    file_path = os.path.join(entities_directory, f"{class_name}.cs")
    with open(file_path, "w") as f:
        f.write("using System;\n\n")
        f.write(f"namespace FugasDetectionSystem.Domain.Entities\n{{\n    public class {class_name}\n    {{\n")
        for column in columns:
            col_name, data_type = column
            # Aquí podrías convertir data_type de SQL a C# si es necesario
            f.write(f"        public string {col_name} {{ get; set; }}\n")  # Ejemplo básico con string
        f.write("    }\n}\n")
    print(f"Generada clase de entidad para: {table_name}")

def generate_repository_class(table_name):
    class_name = f"{table_name}Repository"
    file_path = os.path.join(repositories_directory, f"{class_name}.cs")
    with open(file_path, "w") as f:
        f.write("using System;\nusing System.Collections.Generic;\nusing System.Threading.Tasks;\n")
        f.write(f"using FugasDetectionSystem.Domain.Entities;\nusing FugasDetectionSystem.Domain.Interfaces;\n\n")
        f.write(f"namespace FugasDetectionSystem.Infrastructure.Repositories\n{{\n    public class {class_name} : BaseRepository, I{table_name}Repository\n    {{\n        // Implementación del repositorio\n    }}\n}}\n")
    print(f"Generada clase de repositorio para: {table_name}")

def main():
    # Lista de tablas para generar clases y repositorios
    tables = ["CanalesDeComunicacion", "Clientes", "DisponibilidadTecnico", "Equipos", "Interacciones", "Operadores", "Pagos", "ServicioEquipos", "Servicios", "TecnicoEquipo", "Tecnicos", "Visitas"]
    for table in tables:
        columns = get_table_columns(table)
        generate_entity_class(table, columns)
        generate_repository_class(table)

if __name__ == "__main__":
    main()
