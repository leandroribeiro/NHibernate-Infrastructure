using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace EBXDashboardsModel.Infra.Repositories {
    /// <summary>
    /// Helps with some disposing and closing operations
    /// depending on sent type.
    /// </summary>
    public static class Disposer {
        /// <summary>
        /// Disposes sent object.
        /// </summary>
        /// <param name="obj"></param>
        public static void DisposeInstance(IDisposable obj) {
            if (obj == null)
                return;

            DbCommand dbCmd;
            DbConnection dbCn;
            DbDataReader dbDrdr;
            Stream strm;
            TextReader txtRdr;

            dbCmd = obj as DbCommand;
            if (dbCmd != null) {
                DisposeDbCommand(dbCmd);
                return;
            }

            dbCn = obj as DbConnection;
            if (dbCn != null) {
                DisposeDbConnection(dbCn);
                return;
            }

            dbDrdr = obj as DbDataReader;
            if (dbDrdr != null) {
                DisposeDataReader(dbDrdr);
                return;
            }

            strm = obj as Stream;
            if (strm != null) {
                DisposeStream(strm);
                return;
            }

            txtRdr = obj as TextReader;
            if (txtRdr != null) {
                DisposeTextReader(txtRdr);
                return;
            }

            DoDispose(obj);
        }

        /// <summary>
        /// Closes and disposes sent datareader.
        /// </summary>
        /// <param name="reader"></param>
        public static void DisposeDataReader(IDataReader reader) {
            if (reader == null)
                return;

            reader.Close();
            DoDispose(reader);
        }

        /// <summary>
        /// Disposes sent dbcommand, will not explicity close the connection.
        /// </summary>
        /// <param name="cmd"></param>
        public static void DisposeDbCommand(DbCommand cmd) {
            DisposeDbCommand(cmd, false);
        }

        /// <summary>
        /// Disposes sent dbcommand.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="explicityCloseConnection">Defines if cmd.Connection.Close should be called.</param>
        public static void DisposeDbCommand(DbCommand cmd, bool explicityCloseConnection) {
            if (cmd == null)
                return;

            if (cmd.Connection != null && explicityCloseConnection && cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();

            DoDispose(cmd);
        }

        /// <summary>
        /// Closes and disposes sent connection.
        /// </summary>
        /// <param name="connection"></param>
        public static void DisposeDbConnection(DbConnection connection) {
            if (connection == null)
                return;

            connection.Close();
            DoDispose(connection);
        }

        /// <summary>
        /// Closes and Disposes the sent text reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public static void DisposeTextReader(TextReader reader) {
            if (reader == null)
                return;

            reader.Close();
            DoDispose(reader);
        }

        /// <summary>
        /// Closes and Disposes the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static void DisposeStream(Stream stream) {
            if (stream == null)
                return;

            stream.Close();
            DoDispose(stream);
        }

        /// <summary>
        /// Disposes sent object, but only if it implements IDisposable.
        /// </summary>
        /// <remarks>
        /// If you know that the object implements IDisposable, then
        /// use specific Dispose instead.
        /// </remarks>
        /// <param name="obj"></param>
        public static void DisposeObject(object obj) {
            IDisposable x = (obj as IDisposable);
            if (x == null)
                return;

            DisposeInstance(x);
        }

        private static void DoDispose(IDisposable obj) {
            obj.Dispose();
        }
    }

}
