// Copyright (C) 2002 Mark Pasternak
//
// This software is provided 'as-is', without any express or implied
// warranty.  In no event will the authors be held liable for any damages
// arising from the use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not
//    claim that you wrote the original software. If you use this software
//    in a product, an acknowledgment in the product documentation would be
//    appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be
//    misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.
//
// Mark Pasternak (mark.pasternak@universum.se)


using System;
using System.Web;
using System.IO;

namespace MarkPasternak.Utility
{
	/// <summary>
	/// The HttpResponse.WriteFile method does not work very well with large files since it reads
	/// the whole file to memory at once. This may crash the ASP.NET worker process if the file is too big.
	/// This helper class uses buffers and continuously checks if the client is connected before it sends any output.
	/// </summary>
	public class WriteFileHelper
	{
		private int m_bufferSize=4096;
		private HttpContext Context;
		public EventHandler DownloadCancelled;
		public EventHandler DownloadCompleted;

		public WriteFileHelper()
		{
			Context = HttpContext.Current;
		}

		/// <summary>
		/// Sets and gets the size of the buffer that is used when a file is read to memory.
		/// A larger buffer will require more memory, but will on the other hand make 
		/// it less resource intensive to send a file. Experiment to find a good balance.
		/// </summary>
		public int BufferSize
		{
			set{m_bufferSize=value;}
			get{return m_bufferSize;}
		}

		protected void OnDownloadCancelled() 
		{ 
			Context.Response.OutputStream.Close();
			if (DownloadCancelled != null) 
				DownloadCancelled(null, null); 
		}

		protected void OnDownloadCompleted() 
		{ 
			if (DownloadCompleted != null) 
				DownloadCompleted(null, null); 
		}

		/// <summary>
		/// Writes a file to the Response Stream
		/// </summary>
		/// <param name="filePath">A Path to a file</param>
		public void WriteFileToResponseStream(string filePath)
		{
			if(!File.Exists(filePath))
				throw new Exception("File Path does not exist");
			WriteBinaryFile_Internal(filePath);
		}

		/// <summary>
		/// Writes a file to the Response Stream and adds the headers so the "Save As" dialog is presented to the user
		/// </summary>
		/// <param name="filePath">A Path to a file</param>
		public void WriteFileToResponseStreamWithForceDownloadHeaders(string filePath, string sFileName)
		{
			if(!File.Exists(filePath))
				throw new Exception("File Path does not exist");

			Context.Response.Clear();
			Context.Response.ContentType = "application/octet-stream";
//			Context.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
			Context.Response.AddHeader("Content-Disposition", "attachment; filename=" + sFileName);
			Context.Response.AddHeader("Content-Length", new FileInfo(filePath).Length.ToString());
			WriteBinaryFile_Internal(filePath);
		}

		private void WriteBinaryFile_Internal(string filePath)
		{
			Context.Response.Buffer=false;
			FileStream inStr = null;
			byte[] buffer = new byte[m_bufferSize];
			long byteCount;
			try
			{
				inStr = File.OpenRead(filePath);
				while ((byteCount = inStr.Read(buffer, 0, buffer.Length)) > 0) 
				{
					if(Context.Response.IsClientConnected)
					{
						Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
						Context.Response.Flush();
					}
					else
					{
						OnDownloadCancelled();
						return;
					}
				}
				OnDownloadCompleted();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				inStr.Close();
				Context.Response.End();
			}
		}


	}
}
