using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Hazware.IO
{
  /// <summary>
  /// Stream wrapper class that does not close the stream being 'carried' along. Nearly
  /// all functionality is delegated to the internal stream, except for Close() and 
  /// Dispose(). These two methods are modified to prevent the closing of the stream.
  /// This allows a stream to be used as nested stream, such as with StreamWriter and 
  /// CryptoStream, and not be closed when they complete their usage.
  /// 
  /// This class is derived from public domain work by Stephen Toub (stoub@microsoft.com)
  /// <example>
  /// FileStream fs = new FileStream("test.txt", FileMode.Create);
  /// using(CarrierStream cs = new CarrierStream(fs))
  /// {
  ///   ... // do some work on the stream
  /// }
  /// fs.Write(...);  // the file stream is still valid
  /// </example>
  /// <seealso cref="Stream"/>
  /// </summary>
  public class CarrierStream : Stream
  {
    #region Properties
    /// <summary>
    /// Gets the wrapped stream.
    /// </summary>
    /// <value>The wrapped stream.</value>
    public Stream WrappedStream { get; private set; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="CarrierStream"/> class.
    /// </summary>
    /// <param name="stream">The stream.</param>
    public CarrierStream(Stream stream)
    {
      Contract.Requires<ArgumentNullException>(stream != null);
      WrappedStream = stream;
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Begins an asynchronous read operation.
    /// </summary>
    /// <param name="buffer">The buffer to read the data into.</param>
    /// <param name="offset">The byte offset in buffer at which to begin writing data read from the stream.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
    /// <returns>
    /// An <see cref="T:System.IAsyncResult"></see> that represents the asynchronous read, which could still be pending.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream, or a disk error occurs. </exception>
    /// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the read operation. </exception>
    /// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return (WrappedStream.BeginRead(buffer, offset, count, callback, state));
    }
    /// <summary>
    /// Begins an asynchronous write operation.
    /// </summary>
    /// <param name="buffer">The buffer to write data from.</param>
    /// <param name="offset">The byte offset in buffer from which to begin writing.</param>
    /// <param name="count">The maximum number of bytes to write.</param>
    /// <param name="callback">An optional asynchronous callback, to be called when the write is complete.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
    /// <returns>
    /// An IAsyncResult that represents the asynchronous write, which could still be pending.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the write operation. </exception>
    /// <exception cref="T:System.IO.IOException">Attempted an asynchronous write past the end of the stream, or a disk error occurs. </exception>
    /// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
    {
      return (WrappedStream.BeginWrite(buffer, offset, count, callback, state));
    }
    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
    /// </summary>
    /// <returns>true if the stream supports reading; otherwise, false.</returns>
    public override bool CanRead
    {
      get { return (WrappedStream.CanRead); }
    }
    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
    /// </summary>
    /// <returns>true if the stream supports seeking; otherwise, false.</returns>
    public override bool CanSeek
    {
      get { return (WrappedStream.CanSeek); }
    }
    /// <summary>
    /// Gets a value that determines whether the current stream can time out.
    /// </summary>
    /// <returns>A value that determines whether the current stream can time out.</returns>
    public override bool CanTimeout
    {
      get { return (WrappedStream.CanTimeout); }
    }
    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
    /// </summary>
    /// <returns>true if the stream supports writing; otherwise, false.</returns>
    public override bool CanWrite
    {
      get { return (WrappedStream.CanWrite); }
    }
    /// <summary>
    /// Does not close the current stream. Instead it just returns, leaving the underlying 
    /// stream open.
    /// </summary>
    public override void Close()
    {
      //  NOTE: This is the magic of this class. Close is not passed on to the base
      //  class in order to avoid actually closing the stream.
    }
    /// <summary>
    /// Waits for the pending asynchronous read to complete.
    /// </summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
    /// <returns>
    /// The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">asyncResult did not originate from a <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)"></see> method on the current stream. </exception>
    /// <exception cref="T:System.ArgumentNullException">asyncResult is null. </exception>
    public override int EndRead(IAsyncResult asyncResult)
    {
      return (WrappedStream.EndRead(asyncResult));
    }
    /// <summary>
    /// Ends an asynchronous write operation.
    /// </summary>
    /// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
    /// <exception cref="T:System.ArgumentNullException">asyncResult is null. </exception>
    /// <exception cref="T:System.ArgumentException">asyncResult did not originate from a <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)"></see> method on the current stream. </exception>
    public override void EndWrite(IAsyncResult asyncResult)
    {
      WrappedStream.EndWrite(asyncResult);
    }
    /// <summary>
    /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    public override void Flush()
    {
      WrappedStream.Flush();
    }
    /// <summary>
    /// When overridden in a derived class, gets the length in bytes of the stream.
    /// </summary>
    /// <returns>A long value representing the length of the stream in bytes.</returns>
    /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Length
    {
      get { return (WrappedStream.Length); }
    }
    /// <summary>
    /// When overridden in a derived class, gets or sets the position within the current stream.
    /// </summary>
    /// <returns>The current position within the stream.</returns>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Position
    {
      get { return (WrappedStream.Position); }
      set { WrappedStream.Position = value; }
    }
    /// <summary>
    /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
    /// <returns>
    /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">The sum of offset and count is larger than the buffer length. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
    /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
    public override int Read(byte[] buffer, int offset, int count)
    {
      return (WrappedStream.Read(buffer, offset, count));
    }
    /// <summary>
    /// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
    /// </summary>
    /// <returns>
    /// The unsigned byte cast to an Int32, or -1 if at the end of the stream.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override int ReadByte()
    {
      return (WrappedStream.ReadByte());
    }
    /// <summary>
    /// Gets or sets a value that determines how long the stream will attempt to read before timing out.
    /// </summary>
    /// <returns>A value that determines how long the stream will attempt to read before timing out.</returns>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.ReadTimeout"></see> method always throws an <see cref="T:System.InvalidOperationException"></see>. </exception>
    public override int ReadTimeout
    {
      get { return (WrappedStream.ReadTimeout); }
      set { WrappedStream.ReadTimeout = value; }
    }
    /// <summary>
    /// When overridden in a derived class, sets the position within the current stream.
    /// </summary>
    /// <param name="offset">A byte offset relative to the origin parameter.</param>
    /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
    /// <returns>
    /// The new position within the current stream.
    /// </returns>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
      return (WrappedStream.Seek(offset, origin));
    }
    /// <summary>
    /// When overridden in a derived class, sets the length of the current stream.
    /// </summary>
    /// <param name="value">The desired length of the current stream in bytes.</param>
    /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    public override void SetLength(long value)
    {
      WrappedStream.SetLength(value);
    }
    /// <summary>
    /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
    /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
    /// <param name="count">The number of bytes to be written to the current stream.</param>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
    /// <exception cref="T:System.ArgumentException">The sum of offset and count is greater than the buffer length. </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      WrappedStream.Write(buffer, offset, count);
    }
    /// <summary>
    /// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
    /// </summary>
    /// <param name="value">The byte to write to the stream.</param>
    /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
    /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
    /// <exception cref="T:System.NotSupportedException">The stream does not support writing, or the stream is already closed. </exception>
    public override void WriteByte(byte value)
    {
      WrappedStream.WriteByte(value);
    }
    /// <summary>
    /// Gets or sets a value that determines how long the stream will attempt to write before timing out.
    /// </summary>
    /// <value></value>
    /// <returns>A value that determines how long the stream will attempt to write before timing out.</returns>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.WriteTimeout"></see> method always throws an <see cref="T:System.InvalidOperationException"></see>. </exception>
    public override int WriteTimeout
    {
      get { return (WrappedStream.WriteTimeout); }
      set { WrappedStream.WriteTimeout = value; }
    }
    #endregion

    #region Disposal
    /// <summary>
    /// Normal Dispose functionality would be to close the internal stream. This is
    /// not the functionality desired, so we do not close the internal stream. Instead
    /// we just suppress finalization.
    /// <seealso cref="Close"/>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public new void Dispose()
    { //  Do Nothing
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}
