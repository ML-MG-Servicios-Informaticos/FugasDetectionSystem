/****** Object:  Database [db-okfugas-desa]    Script Date: 24/03/2024 10:05:13 ******/
CREATE DATABASE [db-okfugas-desa]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [db-okfugas-desa] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [db-okfugas-desa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET ARITHABORT OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db-okfugas-desa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db-okfugas-desa] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [db-okfugas-desa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db-okfugas-desa] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [db-okfugas-desa] SET  MULTI_USER 
GO
ALTER DATABASE [db-okfugas-desa] SET ENCRYPTION ON
GO
ALTER DATABASE [db-okfugas-desa] SET QUERY_STORE = ON
GO
ALTER DATABASE [db-okfugas-desa] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
ALTER DATABASE [db-okfugas-desa] SET  READ_WRITE 
GO
