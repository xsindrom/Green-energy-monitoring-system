using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network.GoogleTables
{
    public class GoogleTableRequest : BaseRequest
    {
        public string tableId;
        public string range;
        public MajorDimension majorDimension;
        
        public GoogleTableRequest(string tableId, string range, MajorDimension majorDimension)
        {
            this.tableId = tableId;
            this.range = range;
            this.majorDimension = majorDimension;

            path = string.Format(NetworkConstants.GOOGLE_TABLE_REQUEST_FORMAT, tableId, range, majorDimension, NetworkConstants.GOOGLE_APPI_KEY);
        }

        public GoogleTableRequest(string tableId, string range, MajorDimension majorDimension, string apiKey)
        {
            this.tableId = tableId;
            this.range = range;
            this.majorDimension = majorDimension;

            path = string.Format(NetworkConstants.GOOGLE_TABLE_REQUEST_FORMAT, tableId, range, majorDimension, apiKey);

        }
    }

    public enum MajorDimension { ROWS, COLUMNS }
}