var jsGlobal = (function() { return this || (void 1, eval)('this//# sourceURL=jsGlobal-getter'); })();
var release = true;
Object.defineProperty(Object.prototype, 'asSetPublicProperty', {
    value: function(name, value) {
        this[name] = value;
    }
});
Object.defineProperty(Object.prototype, 'asGetPublicProperty', {
    value: function(name) {
        return this[name];
    }
});
(function polyfillWeakMap() {
    if (typeof jsGlobal.WeakMap === 'function') {
        return;
    }
    var id = 0;

    function WeakMap() {
        this.id = '$weakmap' + (id++);
    };
    WeakMap.prototype = {
        has: function(obj) {
            return obj.hasOwnProperty(this.id);
        },
        get: function(obj, defaultValue) {
            return obj.hasOwnProperty(this.id) ? obj[this.id] : defaultValue;
        },
        set: function(obj, value) {
            Object.defineProperty(obj, this.id, {
                value: value,
                enumerable: false,
                configurable: true
            });
        }
    };
    jsGlobal.WeakMap = WeakMap;
})();
var Shumway;
(function(Shumway) {
    function isNumeric(a) {
        return +a == a;
    }
    Shumway.isNumeric = isNumeric;
})(Shumway || (Shumway = {}));
(function(Shumway) {
    var AVM2;
    (function(AVM2) {
        var Runtime;
        (function(Runtime) {
            function construct(cls, args) {
                return {};
            }
            Runtime.construct = construct;

            function forEachPublicProperty(obj, fn, thisArg = null) {
                for (var i in obj) {
                    fn.call(thisArg, i, obj[i]);
                }
            }
            Runtime.forEachPublicProperty = forEachPublicProperty;
        })(Runtime = AVM2.Runtime || (AVM2.Runtime = {}));
    })(AVM2 = Shumway.AVM2 || (Shumway.AVM2 = {}));
})(Shumway || (Shumway = {}));
(function(Shumway) {
    var AVM2;
    (function(AVM2) {
        var ABC;
        (function(ABC) {
            class Multiname {
                static getQualifiedName(name) {
                    return name;
                }
                static getPublicQualifiedName(name) {
                    return name;
                }
            }
            ABC.Multiname = Multiname;
        })(ABC = AVM2.ABC || (AVM2.ABC = {}));
    })(AVM2 = Shumway.AVM2 || (Shumway.AVM2 = {}));
})(Shumway || (Shumway = {}));
(function(Shumway) {
    var AVM2;
    (function(AVM2) {
        var AS;
        (function(AS) {
            var flash;
            (function(flash) {
                var net;
                (function(net) {
                    class ObjectEncoding {}
                    ObjectEncoding.AMF0 = 0;
                    ObjectEncoding.AMF3 = 3;
                    ObjectEncoding.DEFAULT = ObjectEncoding.AMF3;
                    net.ObjectEncoding = ObjectEncoding;
                })(net = flash.net || (flash.net = {}));
            })(flash = AS.flash || (AS.flash = {}));
        })(AS = AVM2.AS || (AVM2.AS = {}));
    })(AVM2 = Shumway.AVM2 || (Shumway.AVM2 = {}));
})(Shumway || (Shumway = {}));
(function(Shumway) {
    var AVM2;
    (function(AVM2) {
        var AS;
        (function(AS) {
            var flash;
            (function(flash) {
                var utils;
                (function(utils) {
                    class ByteArray {
                        constructor() {
                            return buildByteArray();
                        }
                    }
                    utils.ByteArray = ByteArray;

                    function buildByteArray() {
                        var result = [];
                        Object.defineProperties(result, {
                            _buffer: {
                                get: function() {
                                    return new Uint8Array(this).buffer;
                                }
                            },
                            position: { value: 0, writable: true },
                            objectEncoding: { value: 3, writable: true },
                            readByte: {
                                value: function() {
                                    if (result.position >= result.length) {
                                        throw new Error('EOF');
                                    }
                                    return result[result.position++];
                                }
                            },
                            writeByte: {
                                value: function(v) {
                                    result[result.position++] = v & 0xFF;
                                }
                            },
                            readObject: {
                                value: function() {
                                    switch (this.objectEncoding) {
                                        case flash.net.ObjectEncoding.AMF0:
                                            return AVM2.AMF0.read(this);
                                        case flash.net.ObjectEncoding.AMF3:
                                            return AVM2.AMF3.read(this);
                                        default:
                                            throw new Error("Object Encoding");
                                    }
                                }
                            },
                            writeObject: {
                                value: function(object) {
                                    switch (this.objectEncoding) {
                                        case flash.net.ObjectEncoding.AMF0:
                                            return AVM2.AMF0.write(this, object);
                                        case flash.net.ObjectEncoding.AMF3:
                                            return AVM2.AMF3.write(this, object);
                                        default:
                                            throw new Error("Object Encoding");
                                    }
                                }
                            },
                            writeRawBytes: {
                                value: function(data) {
                                    for (var i = 0, p = this.position; i < data.length; i++) {
                                        this[p++] = data[i];
                                    }
                                    this.position = p;
                                }
                            }
                        });
                        return result;
                    }
                })(utils = flash.utils || (flash.utils = {}));
            })(flash = AS.flash || (AS.flash = {}));
        })(AS = AVM2.AS || (AVM2.AS = {}));
    })(AVM2 = Shumway.AVM2 || (Shumway.AVM2 = {}));
})(Shumway || (Shumway = {}));
(function(Shumway) {
    var StringUtilities;
    (function(StringUtilities) {
        function utf8decode(str) {
            var bytes = new Uint8Array(str.length * 4);
            var b = 0;
            for (var i = 0, j = str.length; i < j; i++) {
                var code = str.charCodeAt(i);
                if (code <= 0x7f) {
                    bytes[b++] = code;
                    continue;
                }
                if (0xD800 <= code && code <= 0xDBFF) {
                    var codeLow = str.charCodeAt(i + 1);
                    if (0xDC00 <= codeLow && codeLow <= 0xDFFF) {
                        code = ((code & 0x3FF) << 10) + (codeLow & 0x3FF) + 0x10000;
                        ++i;
                    }
                }
                if ((code & 0xFFE00000) !== 0) {
                    bytes[b++] = 0xF8 | ((code >>> 24) & 0x03);
                    bytes[b++] = 0x80 | ((code >>> 18) & 0x3F);
                    bytes[b++] = 0x80 | ((code >>> 12) & 0x3F);
                    bytes[b++] = 0x80 | ((code >>> 6) & 0x3F);
                    bytes[b++] = 0x80 | (code & 0x3F);
                } else if ((code & 0xFFFF0000) !== 0) {
                    bytes[b++] = 0xF0 | ((code >>> 18) & 0x07);
                    bytes[b++] = 0x80 | ((code >>> 12) & 0x3F);
                    bytes[b++] = 0x80 | ((code >>> 6) & 0x3F);
                    bytes[b++] = 0x80 | (code & 0x3F);
                } else if ((code & 0xFFFFF800) !== 0) {
                    bytes[b++] = 0xE0 | ((code >>> 12) & 0x0F);
                    bytes[b++] = 0x80 | ((code >>> 6) & 0x3F);
                    bytes[b++] = 0x80 | (code & 0x3F);
                } else {
                    bytes[b++] = 0xC0 | ((code >>> 6) & 0x1F);
                    bytes[b++] = 0x80 | (code & 0x3F);
                }
            }
            return bytes.subarray(0, b);
        }
        StringUtilities.utf8decode = utf8decode;

        function utf8encode(bytes) {
            var j = 0,
                str = "";
            while (j < bytes.length) {
                var b1 = bytes[j++] & 0xFF;
                if (b1 <= 0x7F) {
                    str += String.fromCharCode(b1);
                } else {
                    var currentPrefix = 0xC0;
                    var validBits = 5;
                    do {
                        var mask = (currentPrefix >> 1) | 0x80;
                        if ((b1 & mask) === currentPrefix) {
                            break;
                        }
                        currentPrefix = (currentPrefix >> 1) | 0x80;
                        --validBits;
                    } while (validBits >= 0);
                    if (validBits <= 0) {
                        throw new Error("Invalid UTF8 character");
                    }
                    var code = (b1 & ((1 << validBits) - 1));
                    for (var i = 5; i >= validBits; --i) {
                        var bi = bytes[j++];
                        if ((bi & 0xC0) !== 0x80) {
                            throw new Error("Invalid UTF8 character sequence");
                        }
                        code = (code << 6) | (bi & 0x3F);
                    }
                    if (code >= 0x10000) {
                        str += String.fromCharCode((((code - 0x10000) >> 10) & 0x3FF) |
                            0xD800, (code & 0x3FF) | 0xDC00);
                    } else {
                        str += String.fromCharCode(code);
                    }
                }
            }
            return str;
        }
        StringUtilities.utf8encode = utf8encode;
    })(StringUtilities = Shumway.StringUtilities || (Shumway.StringUtilities = {}));
})(Shumway || (Shumway = {}));
var Shumway;
(function(Shumway) {
    var AVM2;
    (function(AVM2) {
        var Multiname = Shumway.AVM2.ABC.Multiname;
        var forEachPublicProperty = Shumway.AVM2.Runtime.forEachPublicProperty;
        var construct = Shumway.AVM2.Runtime.construct;
        let AMF0Marker;
        (function(AMF0Marker) {
            AMF0Marker[AMF0Marker["NUMBER"] = 0] = "NUMBER";
            AMF0Marker[AMF0Marker["BOOLEAN"] = 1] = "BOOLEAN";
            AMF0Marker[AMF0Marker["STRING"] = 2] = "STRING";
            AMF0Marker[AMF0Marker["OBJECT"] = 3] = "OBJECT";
            AMF0Marker[AMF0Marker["NULL"] = 5] = "NULL";
            AMF0Marker[AMF0Marker["UNDEFINED"] = 6] = "UNDEFINED";
            AMF0Marker[AMF0Marker["REFERENCE"] = 7] = "REFERENCE";
            AMF0Marker[AMF0Marker["ECMA_ARRAY"] = 8] = "ECMA_ARRAY";
            AMF0Marker[AMF0Marker["OBJECT_END"] = 9] = "OBJECT_END";
            AMF0Marker[AMF0Marker["STRICT_ARRAY"] = 10] = "STRICT_ARRAY";
            AMF0Marker[AMF0Marker["DATE"] = 11] = "DATE";
            AMF0Marker[AMF0Marker["LONG_STRING"] = 12] = "LONG_STRING";
            AMF0Marker[AMF0Marker["XML"] = 15] = "XML";
            AMF0Marker[AMF0Marker["TYPED_OBJECT"] = 16] = "TYPED_OBJECT";
            AMF0Marker[AMF0Marker["AVMPLUS"] = 17] = "AVMPLUS";
        })(AMF0Marker = AVM2.AMF0Marker || (AVM2.AMF0Marker = {}));

        function writeString(ba, s) {
            if (s.length > 0xFFFF) {
                throw 'AMF short string exceeded';
            }
            if (!s.length) {
                ba.writeByte(0x00);
                ba.writeByte(0x00);
                return;
            }
            var bytes = Shumway.StringUtilities.utf8decode(s);
            ba.writeByte((bytes.length >> 8) & 255);
            ba.writeByte(bytes.length & 255);
            for (var i = 0; i < bytes.length; i++) {
                ba.writeByte(bytes[i]);
            }
        }

        function readString(ba) {
            var byteLength = (ba.readByte() << 8) | ba.readByte();
            if (!byteLength) {
                return '';
            }
            var buffer = new Uint8Array(byteLength);
            for (var i = 0; i < byteLength; i++) {
                buffer[i] = ba.readByte();
            }
            return Shumway.StringUtilities.utf8encode(buffer);
        }

        function writeDouble(ba, value) {
            var buffer = new ArrayBuffer(8);
            var view = new DataView(buffer);
            view.setFloat64(0, value, false);
            for (var i = 0; i < buffer.byteLength; i++) {
                ba.writeByte(view.getUint8(i));
            }
        }

        function readDouble(ba) {
            var buffer = new ArrayBuffer(8);
            var view = new DataView(buffer);
            for (var i = 0; i < buffer.byteLength; i++) {
                view.setUint8(i, ba.readByte());
            }
            return view.getFloat64(0, false);
        }

        function setAvmProperty(obj, propertyName, value) {
            obj.asSetPublicProperty(propertyName, value);
        }
        class AMF0 {
            static write(ba, obj) {
                switch (typeof obj) {
                    case 'boolean':
                        ba.writeByte(AMF0Marker.BOOLEAN);
                        ba.writeByte(obj ? 0x01 : 0x00);
                        break;
                    case 'number':
                        ba.writeByte(AMF0Marker.NUMBER);
                        writeDouble(ba, obj);
                        break;
                    case 'undefined':
                        ba.writeByte(AMF0Marker.UNDEFINED);
                        break;
                    case 'string':
                        ba.writeByte(AMF0Marker.STRING);
                        writeString(ba, obj);
                        break;
                    case 'object':
                        if (obj === null) {
                            ba.writeByte(AMF0Marker.NULL);
                        } else if (Array.isArray(obj)) {
                            ba.writeByte(AMF0Marker.ECMA_ARRAY);
                            ba.writeByte((obj.length >>> 24) & 255);
                            ba.writeByte((obj.length >> 16) & 255);
                            ba.writeByte((obj.length >> 8) & 255);
                            ba.writeByte(obj.length & 255);
                            forEachPublicProperty(obj, function(key, value) {
                                writeString(ba, key);
                                this.write(ba, value);
                            }, this);
                            ba.writeByte(0x00);
                            ba.writeByte(0x00);
                            ba.writeByte(AMF0Marker.OBJECT_END);
                        } else {
                            ba.writeByte(AMF0Marker.OBJECT);
                            forEachPublicProperty(obj, function(key, value) {
                                writeString(ba, key);
                                this.write(ba, value);
                            }, this);
                            ba.writeByte(0x00);
                            ba.writeByte(0x00);
                            ba.writeByte(AMF0Marker.OBJECT_END);
                        }
                        return;
                }
            }
            static read(ba) {
                var marker = ba.readByte();
                switch (marker) {
                    case AMF0Marker.NUMBER:
                        return readDouble(ba);
                    case AMF0Marker.BOOLEAN:
                        return !!ba.readByte();
                    case AMF0Marker.STRING:
                        return readString(ba);
                    case AMF0Marker.OBJECT:
                        var obj = {};
                        while (true) {
                            var key = readString(ba);
                            if (!key.length)
                                break;
                            setAvmProperty(obj, key, this.read(ba));
                        }
                        if (ba.readByte() !== AMF0Marker.OBJECT_END) {
                            throw 'AMF0 End marker is not found';
                        }
                        return obj;
                    case AMF0Marker.NULL:
                        return null;
                    case AMF0Marker.UNDEFINED:
                        return undefined;
                    case AMF0Marker.ECMA_ARRAY:
                        var arr = [];
                        arr.length = (ba.readByte() << 24) | (ba.readByte() << 16) |
                            (ba.readByte() << 8) | ba.readByte();
                        while (true) {
                            var key = readString(ba);
                            if (!key.length)
                                break;
                            setAvmProperty(arr, key, this.read(ba));
                        }
                        if (ba.readByte() !== AMF0Marker.OBJECT_END) {
                            throw 'AMF0 End marker is not found';
                        }
                        return arr;
                    case AMF0Marker.STRICT_ARRAY:
                        var arr = [];
                        arr.length = (ba.readByte() << 24) | (ba.readByte() << 16) |
                            (ba.readByte() << 8) | ba.readByte();
                        for (var i = 0; i < arr.length; i++) {
                            arr[i] = this.read(ba);
                        }
                        return arr;
                    case AMF0Marker.AVMPLUS:
                        return readAmf3Data(ba, {});
                    default:
                        throw 'AMF0 Unknown marker ' + marker;
                }
            }
        }
        AVM2.AMF0 = AMF0;
        let AMF3Marker;
        (function(AMF3Marker) {
            AMF3Marker[AMF3Marker["UNDEFINED"] = 0] = "UNDEFINED";
            AMF3Marker[AMF3Marker["NULL"] = 1] = "NULL";
            AMF3Marker[AMF3Marker["FALSE"] = 2] = "FALSE";
            AMF3Marker[AMF3Marker["TRUE"] = 3] = "TRUE";
            AMF3Marker[AMF3Marker["INTEGER"] = 4] = "INTEGER";
            AMF3Marker[AMF3Marker["DOUBLE"] = 5] = "DOUBLE";
            AMF3Marker[AMF3Marker["STRING"] = 6] = "STRING";
            AMF3Marker[AMF3Marker["XML_DOC"] = 7] = "XML_DOC";
            AMF3Marker[AMF3Marker["DATE"] = 8] = "DATE";
            AMF3Marker[AMF3Marker["ARRAY"] = 9] = "ARRAY";
            AMF3Marker[AMF3Marker["OBJECT"] = 10] = "OBJECT";
            AMF3Marker[AMF3Marker["XML"] = 11] = "XML";
            AMF3Marker[AMF3Marker["BYTEARRAY"] = 12] = "BYTEARRAY";
            AMF3Marker[AMF3Marker["VECTOR_INT"] = 13] = "VECTOR_INT";
            AMF3Marker[AMF3Marker["VECTOR_UINT"] = 14] = "VECTOR_UINT";
            AMF3Marker[AMF3Marker["VECTOR_DOUBLE"] = 15] = "VECTOR_DOUBLE";
            AMF3Marker[AMF3Marker["VECTOR_OBJECT"] = 16] = "VECTOR_OBJECT";
            AMF3Marker[AMF3Marker["DICTIONARY"] = 17] = "DICTIONARY";
        })(AMF3Marker = AVM2.AMF3Marker || (AVM2.AMF3Marker = {}));

        function readU29(ba) {
            var b1 = ba.readByte();
            if ((b1 & 0x80) === 0) {
                return b1;
            }
            var b2 = ba.readByte();
            if ((b2 & 0x80) === 0) {
                return ((b1 & 0x7F) << 7) | b2;
            }
            var b3 = ba.readByte();
            if ((b3 & 0x80) === 0) {
                return ((b1 & 0x7F) << 14) | ((b2 & 0x7F) << 7) | b3;
            }
            var b4 = ba.readByte();
            return ((b1 & 0x7F) << 22) | ((b2 & 0x7F) << 15) | ((b3 & 0x7F) << 8) | b4;
        }

        function writeU29(ba, value) {
            if ((value & 0xFFFFFF80) === 0) {
                ba.writeByte(value & 0x7F);
            } else if ((value & 0xFFFFC000) === 0) {
                ba.writeByte(0x80 | ((value >> 7) & 0x7F));
                ba.writeByte(value & 0x7F);
            } else if ((value & 0xFFE00000) === 0) {
                ba.writeByte(0x80 | ((value >> 14) & 0x7F));
                ba.writeByte(0x80 | ((value >> 7) & 0x7F));
                ba.writeByte(value & 0x7F);
            } else if ((value & 0xC0000000) === 0) {
                ba.writeByte(0x80 | ((value >> 22) & 0x7F));
                ba.writeByte(0x80 | ((value >> 15) & 0x7F));
                ba.writeByte(0x80 | ((value >> 8) & 0x7F));
                ba.writeByte(value & 0xFF);
            } else {
                throw 'AMF3 U29 range';
            }
        }

        function readUTF8vr(ba, caches) {
            var u29s = readU29(ba);
            if (u29s === 0x01) {
                return '';
            }
            var stringsCache = caches.stringsCache || (caches.stringsCache = []);
            if ((u29s & 1) === 0) {
                return stringsCache[u29s >> 1];
            }
            var byteLength = u29s >> 1;
            var buffer = new Uint8Array(byteLength);
            for (var i = 0; i < byteLength; i++) {
                buffer[i] = ba.readByte();
            }
            var value = Shumway.StringUtilities.utf8encode(buffer);
            stringsCache.push(value);
            return value;
        }

        function writeUTF8vr(ba, value, caches) {
            if (value === '') {
                ba.writeByte(0x01);
                return;
            }
            var stringsCache = caches.stringsCache || (caches.stringsCache = []);
            var index = stringsCache.indexOf(value);
            if (index >= 0) {
                writeU29(ba, index << 1);
                return;
            }
            stringsCache.push(value);
            var bytes = Shumway.StringUtilities.utf8decode(value);
            writeU29(ba, 1 | (bytes.length << 1));
            for (var i = 0; i < bytes.length; i++) {
                ba.writeByte(bytes[i]);
            }
        }

        function readAmf3Data(ba, caches) {
            var marker = ba.readByte();
            switch (marker) {
                case AMF3Marker.NULL:
                    return null;
                case AMF3Marker.UNDEFINED:
                    return undefined;
                case AMF3Marker.FALSE:
                    return false;
                case AMF3Marker.TRUE:
                    return true;
                case AMF3Marker.INTEGER:
                    return readU29(ba);
                case AMF3Marker.DOUBLE:
                    return readDouble(ba);
                case AMF3Marker.STRING:
                    return readUTF8vr(ba, caches);
                case AMF3Marker.DATE:
                    return new Date(readDouble(ba));
                case AMF3Marker.OBJECT:
                    var u29o = readU29(ba);
                    if ((u29o & 1) === 0) {
                        return caches.objectsCache[u29o >> 1];
                    }
                    if ((u29o & 4) !== 0) {
                        throw 'AMF3 Traits-Ext is not supported';
                    }
                    var traits, objectClass;
                    if ((u29o & 2) === 0) {
                        traits = caches.traitsCache[u29o >> 2];
                        objectClass = traits.class;
                    } else {
                        traits = {};
                        var aliasName = readUTF8vr(ba, caches);
                        traits.className = aliasName;
                        objectClass = aliasName && AVM2.aliasesCache.names[aliasName];
                        traits.class = objectClass;
                        traits.isDynamic = (u29o & 8) !== 0;
                        traits.members = [];
                        var slots = objectClass && objectClass.instanceBindings.slots;
                        for (var i = 0, j = u29o >> 4; i < j; i++) {
                            var traitName = readUTF8vr(ba, caches);
                            var slot = null;
                            for (var j = 1; slots && j < slots.length; j++) {
                                if (slots[j].name.name === traitName) {
                                    slot = slots[j];
                                    break;
                                }
                            }
                            traits.members.push(slot ? Multiname.getQualifiedName(slot.name) :
                                Multiname.getPublicQualifiedName(traitName));
                        }
                        (caches.traitsCache || (caches.traitsCache = [])).push(traits);
                    }
                    var obj = objectClass ? construct(objectClass, []) : {};
                    (caches.objectsCache || (caches.objectsCache = [])).push(obj);
                    for (var i = 0; i < traits.members.length; i++) {
                        var value = readAmf3Data(ba, caches);
                        obj[traits.members[i]] = value;
                    }
                    if (traits.isDynamic) {
                        while (true) {
                            var key = readUTF8vr(ba, caches);
                            if (!key.length)
                                break;
                            var value = readAmf3Data(ba, caches);
                            setAvmProperty(obj, key, value);
                        }
                    }
                    return obj;
                case AMF3Marker.ARRAY:
                    var u29o = readU29(ba);
                    if ((u29o & 1) === 0) {
                        return caches.objectsCache[u29o >> 1];
                    }
                    var arr = [];
                    (caches.objectsCache || (caches.objectsCache = [])).push(arr);
                    var densePortionLength = u29o >> 1;
                    while (true) {
                        var key = readUTF8vr(ba, caches);
                        if (!key.length)
                            break;
                        var value = readAmf3Data(ba, caches);
                        setAvmProperty(arr, key, value);
                    }
                    for (var i = 0; i < densePortionLength; i++) {
                        var value = readAmf3Data(ba, caches);
                        setAvmProperty(arr, i, value);
                    }
                    return arr;
                default:
                    throw 'AMF3 Unknown marker ' + marker;
            }
        }

        function writeCachedReference(ba, obj, caches) {
            var objectsCache = caches.objectsCache || (caches.objectsCache = []);
            var index = objectsCache.indexOf(obj);
            if (index < 0) {
                objectsCache.push(obj);
                return false;
            }
            writeU29(ba, index << 1);
            return true;
        }

        function writeAmf3Data(ba, obj, caches) {
            switch (typeof obj) {
                case 'boolean':
                    ba.writeByte(obj ? AMF3Marker.TRUE : AMF3Marker.FALSE);
                    break;
                case 'number':
                    if (obj === (obj | 0)) {
                        ba.writeByte(AMF3Marker.INTEGER);
                        writeU29(ba, obj);
                    } else {
                        ba.writeByte(AMF3Marker.DOUBLE);
                        writeDouble(ba, obj);
                    }
                    break;
                case 'undefined':
                    ba.writeByte(AMF3Marker.UNDEFINED);
                    break;
                case 'string':
                    ba.writeByte(AMF3Marker.STRING);
                    writeUTF8vr(ba, obj, caches);
                    break;
                case 'object':
                    if (obj === null) {
                        ba.writeByte(AMF3Marker.NULL);
                    } else if (Array.isArray(obj)) {
                        ba.writeByte(AMF3Marker.ARRAY);
                        if (writeCachedReference(ba, obj, caches))
                            break;
                        var densePortionLength = 0;
                        while (densePortionLength in obj) {
                            ++densePortionLength;
                        }
                        writeU29(ba, (densePortionLength << 1) | 1);
                        forEachPublicProperty(obj, function(i, value) {
                            if (Shumway.isNumeric(i) && i >= 0 && i < densePortionLength) {
                                return;
                            }
                            writeUTF8vr(ba, i, caches);
                            writeAmf3Data(ba, value, caches);
                        });
                        writeUTF8vr(ba, '', caches);
                        for (var j = 0; j < densePortionLength; j++) {
                            writeAmf3Data(ba, obj[j], caches);
                        }
                    } else if (obj instanceof Date) {
                        ba.writeByte(AMF3Marker.DATE);
                        if (writeCachedReference(ba, obj, caches))
                            break;
                        writeU29(ba, 1);
                        writeDouble(ba, obj.valueOf());
                    } else {
                        ba.writeByte(AMF3Marker.OBJECT);
                        if (writeCachedReference(ba, obj, caches))
                            break;
                        var isDynamic = true;
                        var objectClass = obj.class;
                        if (objectClass) {
                            isDynamic = !objectClass.classInfo.instanceInfo.isSealed();
                            var aliasName = AVM2.aliasesCache.classes.get(objectClass) || '';
                            var traits, traitsCount;
                            var traitsCache = caches.traitsCache || (caches.traitsCache = []);
                            var traitsInfos = caches.traitsInfos || (caches.traitsInfos = []);
                            var traitsRef = traitsCache.indexOf(objectClass);
                            if (traitsRef < 0) {
                                var slots = objectClass.instanceBindings.slots;
                                traits = [];
                                var traitsNames = [];
                                for (var i = 1; i < slots.length; i++) {
                                    var slot = slots[i];
                                    if (!slot.name.getNamespace().isPublic()) {
                                        continue;
                                    }
                                    traits.push(Multiname.getQualifiedName(slot.name));
                                    traitsNames.push(slot.name.name);
                                }
                                traitsCache.push(objectClass);
                                traitsInfos.push(traits);
                                traitsCount = traitsNames.length;
                                writeU29(ba, (isDynamic ? 0x0B : 0x03) + (traitsCount << 4));
                                writeUTF8vr(ba, aliasName, caches);
                                for (var i = 0; i < traitsCount; i++) {
                                    writeUTF8vr(ba, traitsNames[i], caches);
                                }
                            } else {
                                traits = traitsInfos[traitsRef];
                                traitsCount = traits.length;
                                writeU29(ba, 0x01 + (traitsRef << 2));
                            }
                            for (var i = 0; i < traitsCount; i++) {
                                writeAmf3Data(ba, obj[traits[i]], caches);
                            }
                        } else {
                            writeU29(ba, 0x0B);
                            writeUTF8vr(ba, '', caches);
                        }
                        if (isDynamic) {
                            forEachPublicProperty(obj, function(i, value) {
                                writeUTF8vr(ba, i, caches);
                                writeAmf3Data(ba, value, caches);
                            });
                            writeUTF8vr(ba, '', caches);
                        }
                    }
                    return;
            }
        }
        AVM2.aliasesCache = {
            classes: new WeakMap(),
            names: Object.create(null)
        };
        class AMF3 {
            static write(ba, object) {
                writeAmf3Data(ba, object, {});
            }
            static read(ba) {
                return readAmf3Data(ba, {});
            }
        }
        AVM2.AMF3 = AMF3;
    })(AVM2 = Shumway.AVM2 || (Shumway.AVM2 = {}));
})(Shumway || (Shumway = {}));
var RtmpJs;
(function(RtmpJs) {
    //@NOTE: Pravin, we can change this
    var MAX_CHUNKED_CHANNEL_BUFFER = 0x80000;
    var RANDOM_DATA_SIZE = 1536;
    var PROTOCOL_VERSION = 3;
    var SET_CHUNK_SIZE_CONTROL_MESSAGE_ID = 1;
    var ABORT_MESSAGE_CONTROL_MESSAGE_ID = 2;
    var ACK_MESSAGE_ID = 3;
    var USER_CONTROL_MESSAGE_ID = 4;
    var ACK_WINDOW_SIZE_MESSAGE_ID = 5;
    var SET_PEER_BANDWIDTH_MESSAGE_ID = 6;
    var CONTROL_CHUNK_STREAM_ID = 2;
    var MIN_CHUNK_STREAM_ID = 3;
    var MAX_CHUNK_STREAM_ID = 65599;
    var MAX_CHUNK_HEADER_SIZE = 18;
    class ChunkedStream {
        constructor(id) {
            this.onmessage = null;
            this.id = id;
            this.buffer = null;
            this.bufferLength = 0;
            this.lastStreamId = -1;
            this.lastTimestamp = 0;
            this.lastLength = 0;
            this.lastTypeId = 0;
            this.lastMessageComplete = false;
            this.waitingForBytes = 0;
            this.sentStreamId = -1;
            this.sentTimestamp = 0;
            this.sentLength = 0;
            this.sentTypeId = 0;
        }
        setBuffer(enabled) {
            if (enabled && !this.buffer) {
                //@NOTE: Pravin, we can change this
                this.buffer = new Uint8Array(128);
                this.bufferLength = 0;
            }
            if (!enabled && this.buffer) {
                this.buffer = null;
                this.bufferLength = 0;
            }
        }
        abort() {
            if (this.buffer) {
                this.bufferLength = 0;
            } else if (!this.lastMessageComplete) {
                this.lastMessageComplete = true;
                this.onmessage({
                    timestamp: this.lastTimestamp,
                    streamId: this.lastStreamId,
                    chunkedStreamId: this.id,
                    typeId: this.lastTypeId,
                    data: null,
                    firstChunk: false,
                    lastChunk: true
                });
            }
        }
        _push(data, firstChunk, lastChunk) {
            if (!this.onmessage) {
                return;
            }
            if ((firstChunk && lastChunk) || !this.buffer) {
                this.onmessage({
                    timestamp: this.lastTimestamp,
                    streamId: this.lastStreamId,
                    chunkedStreamId: this.id,
                    typeId: this.lastTypeId,
                    data: data,
                    firstChunk: firstChunk,
                    lastChunk: lastChunk
                });
                return;
            }
            if (firstChunk) {
                this.bufferLength = 0;
                if (this.lastLength > this.buffer.length) {
                    this.buffer = new Uint8Array(this.lastLength);
                }
            }
            this.buffer.set(data, this.bufferLength);
            this.bufferLength += data.length;
            if (lastChunk) {
                this.onmessage({
                    timestamp: this.lastTimestamp,
                    streamId: this.lastStreamId,
                    chunkedStreamId: this.id,
                    typeId: this.lastTypeId,
                    data: this.buffer.subarray(0, this.bufferLength),
                    firstChunk: true,
                    lastChunk: true
                });
            }
        }
    }
    RtmpJs.ChunkedStream = ChunkedStream;
    class ChunkedChannel {
        constructor() {
            //@NOTE: Pravin, we can change this
            this.onusercontrolmessage = null;
            this.onack = null;
            this.ondata = function(data) {};
            this.onclose = function() {};
            this.oncreated = null;
            this.state = 'uninitialized';
            this.buffer = new Uint8Array(4092);
            this.bufferLength = 0;
            this.chunkSize = 128;
            this.chunkStreams = [];
            this.peerChunkSize = 128;
            this.peerAckWindowSize = 0;
            this.bandwidthLimitType = 0;
            this.windowAckSize = 0;
            this.bytesReceived = 0;
            this.lastAckSent = 0;
        }
        push(data) {
            var newDataLength = data.length + this.bufferLength;
            if (newDataLength > this.buffer.length) {
                var newBufferLength = this.buffer.length * 2;
                while (newDataLength > newBufferLength) {
                    newBufferLength *= 2;
                }
                if (newBufferLength > MAX_CHUNKED_CHANNEL_BUFFER) {
                    this._fail('Buffer overflow');
                }
                var newBuffer = new Uint8Array(newBufferLength);
                newBuffer.set(this.buffer);
                this.buffer = newBuffer;
            }
            for (var i = 0, j = this.bufferLength; i < data.length; i++, j++) {
                this.buffer[j] = data[i];
            }
            this.bufferLength = newDataLength;
            this.bytesReceived += data.length;
            if (this.peerAckWindowSize &&
                (this.bytesReceived - this.lastAckSent) >= this.peerAckWindowSize) {
                this._sendAck();
            }
            while (this.bufferLength > 0) {
                var shiftBy = 0;
                switch (this.state) {
                    case 'uninitialized':
                        if (this.bufferLength < 1) {
                            return;
                        }
                        this.serverVersion = this.buffer[0];
                        shiftBy = 1;
                        if (this.serverVersion !== PROTOCOL_VERSION) {
                            this._fail('Unsupported protocol version: ' + this.serverVersion);
                        }
                        this.state = 'version_received';
                        break;
                    case 'version_received':
                        if (this.bufferLength < RANDOM_DATA_SIZE) {
                            return;
                        }
                        shiftBy = RANDOM_DATA_SIZE;
                        var timestamp = (Date.now() - this.epochStart) | 0;
                        this.buffer[4] = (timestamp >>> 24) & 0xFF;
                        this.buffer[5] = (timestamp >>> 16) & 0xFF;
                        this.buffer[6] = (timestamp >>> 8) & 0xFF;
                        this.buffer[7] = timestamp & 0xFF;
                        this.ondata(this.buffer.subarray(0, RANDOM_DATA_SIZE));
                        this.state = 'ack_sent';
                        break;
                    case 'ack_sent':
                        if (this.bufferLength < RANDOM_DATA_SIZE) {
                            return;
                        }
                        shiftBy = RANDOM_DATA_SIZE;
                        for (var i = 8; i < RANDOM_DATA_SIZE; i++) {
                            if (this.buffer[i] !== this.randomData[i]) {
                                this._fail('Random data do not match @' + i);
                            }
                        }
                        this.state = 'handshake_done';
                        this.lastAckSent = this.bytesReceived;
                        this._initialize();
                        break;
                    case 'handshake_done':
                        shiftBy = this._parseChunkedData();
                        if (!shiftBy) {
                            return;
                        }
                        break;
                    default:
                        return;
                }
                this.buffer.set(this.buffer.subarray(shiftBy, this.bufferLength), 0);
                this.bufferLength -= shiftBy;
            }
        }
        _initialize() {
            var controlStream = this._getChunkStream(CONTROL_CHUNK_STREAM_ID);
            controlStream.setBuffer(true);
            controlStream.onmessage = function(e) {
                if (e.streamId !== 0) {
                    return;
                }
                release || console.log('Control message: ' + e.typeId);
                switch (e.typeId) {
                    case SET_CHUNK_SIZE_CONTROL_MESSAGE_ID:
                        var newChunkSize = (e.data[0] << 24) | (e.data[1] << 16) |
                            (e.data[2] << 8) | e.data[3];
                        if (newChunkSize >= 1 && newChunkSize <= 0x7FFFFFFF) {
                            this.peerChunkSize = newChunkSize;
                        }
                        break;
                    case ABORT_MESSAGE_CONTROL_MESSAGE_ID:
                        var chunkStreamId = (e.data[0] << 24) | (e.data[1] << 16) |
                            (e.data[2] << 8) | e.data[3];
                        if (MIN_CHUNK_STREAM_ID <= chunkStreamId &&
                            chunkStreamId <= MAX_CHUNK_STREAM_ID) {
                            var chunkStream = this._getChunkStream(chunkStreamId);
                            chunkStream.abort();
                        }
                        break;
                    case ACK_MESSAGE_ID:
                        if (this.onack) {
                            this.onack();
                        }
                        break;
                    case USER_CONTROL_MESSAGE_ID:
                        if (this.onusercontrolmessage) {
                            this.onusercontrolmessage({
                                type: (e.data[0] << 8) | e.data[1],
                                data: e.data.subarray(2)
                            });
                        }
                        break;
                    case ACK_WINDOW_SIZE_MESSAGE_ID:
                        var ackWindowSize = (e.data[0] << 24) | (e.data[1] << 16) |
                            (e.data[2] << 8) | e.data[3];
                        if (ackWindowSize < 0) {
                            break;
                        }
                        this.peerAckWindowSize = ackWindowSize;
                        break;
                    case SET_PEER_BANDWIDTH_MESSAGE_ID:
                        var ackWindowSize = (e.data[0] << 24) | (e.data[1] << 16) |
                            (e.data[2] << 8) | e.data[3];
                        var limitType = e.data[4];
                        if (ackWindowSize < 0 || limitType > 2) {
                            break;
                        }
                        if (limitType === 1 ||
                            (limitType === 2 && this.bandwidthLimitType === 1)) {
                            ackWindowSize = Math.min(this.windowAckSize, ackWindowSize);
                        }
                        if (ackWindowSize !== this.ackWindowSize) {
                            this.ackWindowSize = ackWindowSize;
                            var ackData = new Uint8Array([(ackWindowSize >>> 24) & 0xFF,
                                (ackWindowSize >>> 16) & 0xFF,
                                (ackWindowSize >>> 8) & 0xFF,
                                ackWindowSize & 0xFF
                            ]);
                            this._sendMessage(CONTROL_CHUNK_STREAM_ID, {
                                typeId: ACK_WINDOW_SIZE_MESSAGE_ID,
                                streamId: 0,
                                data: ackData
                            });
                            if (limitType !== 2) {
                                this.bandwidthLimitType = limitType;
                            }
                        }
                        break;
                }
            }.bind(this);
            if (this.oncreated) {
                this.oncreated();
            }
        }
        setChunkSize(chunkSize) {
            if (chunkSize < 1 || chunkSize > 0x7FFFFFFF) {
                throw new Error('Invalid chunk size');
            }
            this._sendMessage(CONTROL_CHUNK_STREAM_ID, {
                streamId: 0,
                typeId: SET_CHUNK_SIZE_CONTROL_MESSAGE_ID,
                data: new Uint8Array([(chunkSize >>> 24) & 0xFF,
                    (chunkSize >>> 16) & 0xFF,
                    (chunkSize >>> 8) & 0xFF,
                    chunkSize & 0xFF
                ])
            });
            this.chunkSize = chunkSize;
        }
        send(chunkStreamId, message) {
            if (chunkStreamId < MIN_CHUNK_STREAM_ID ||
                chunkStreamId > MAX_CHUNK_STREAM_ID) {
                throw new Error('Invalid chunkStreamId');
            }
            return this._sendMessage(chunkStreamId, message);
        }
        sendUserControlMessage(type, data) {
            var eventData = new Uint8Array(2 + data.length);
            eventData[0] = (type >> 8) & 0xFF;
            eventData[1] = type & 0xFF;
            eventData.set(data, 2);
            this._sendMessage(CONTROL_CHUNK_STREAM_ID, {
                typeId: USER_CONTROL_MESSAGE_ID,
                streamId: 0,
                data: eventData
            });
        }
        _sendAck() {
            var ackData = new Uint8Array([(this.bytesReceived >>> 24) & 0xFF,
                (this.bytesReceived >>> 16) & 0xFF,
                (this.bytesReceived >>> 8) & 0xFF,
                this.bytesReceived & 0xFF
            ]);
            this._sendMessage(CONTROL_CHUNK_STREAM_ID, {
                typeId: ACK_MESSAGE_ID,
                streamId: 0,
                data: ackData
            });
        }
        _sendMessage(chunkStreamId, message) {
            var data = message.data;
            var messageLength = data.length;
            var chunkStream = this._getChunkStream(chunkStreamId);
            var timestamp = ('timestamp' in message ? message.timestamp : (Date.now() - this.epochStart)) | 0;
            var timestampDelta = (timestamp - chunkStream.sentTimestamp) | 0;
            var buffer = new Uint8Array(this.chunkSize + MAX_CHUNK_HEADER_SIZE);
            var chunkStreamIdSize;
            if (chunkStreamId < 64) {
                chunkStreamIdSize = 1;
                buffer[0] = chunkStreamId;
            } else if (chunkStreamId < 320) {
                chunkStreamIdSize = 2;
                buffer[0] = 0;
                buffer[1] = chunkStreamId - 64;
            } else {
                chunkStreamIdSize = 3;
                buffer[0] = 1;
                buffer[1] = ((chunkStreamId - 64) >> 8) & 0xFF;
                buffer[2] = (chunkStreamId - 64) & 0xFF;
            }
            var position = chunkStreamIdSize;
            var extendTimestamp = 0;
            if (message.streamId !== chunkStream.sentStreamId || timestampDelta < 0) {
                if ((timestamp & 0xFF000000) !== 0) {
                    extendTimestamp = timestamp;
                    buffer[position] = buffer[position + 1] = buffer[position + 2] = 0xFF;
                } else {
                    buffer[position] = (timestamp >> 16) & 0xFF;
                    buffer[position + 1] = (timestamp >> 8) & 0xFF;
                    buffer[position + 2] = timestamp & 0xFF;
                }
                position += 3;
                buffer[position++] = (messageLength >> 16) & 0xFF;
                buffer[position++] = (messageLength >> 8) & 0xFF;
                buffer[position++] = messageLength & 0xFF;
                buffer[position++] = message.typeId;
                buffer[position++] = message.streamId & 0xFF;
                buffer[position++] = (message.streamId >> 8) & 0xFF;
                buffer[position++] = (message.streamId >> 16) & 0xFF;
                buffer[position++] = (message.streamId >> 24) & 0xFF;
            } else if (messageLength !== chunkStream.sentLength ||
                message.typeId !== chunkStream.sentTypeId) {
                buffer[0] |= 0x40;
                if ((timestampDelta & 0xFF000000) !== 0) {
                    extendTimestamp = timestampDelta;
                    buffer[position] = buffer[position + 1] = buffer[position + 2] = 0xFF;
                } else {
                    buffer[position] = (timestampDelta >> 16) & 0xFF;
                    buffer[position + 1] = (timestampDelta >> 8) & 0xFF;
                    buffer[position + 2] = timestampDelta & 0xFF;
                }
                position += 3;
                buffer[position++] = (messageLength >> 16) & 0xFF;
                buffer[position++] = (messageLength >> 8) & 0xFF;
                buffer[position++] = messageLength & 0xFF;
                buffer[position++] = message.typeId;
            } else if (timestampDelta !== 0) {
                buffer[0] |= 0x80;
                if ((timestampDelta & 0xFF000000) !== 0) {
                    extendTimestamp = timestampDelta;
                    buffer[position] = buffer[position + 1] = buffer[position + 2] = 0xFF;
                } else {
                    buffer[position] = (timestampDelta >> 16) & 0xFF;
                    buffer[position + 1] = (timestampDelta >> 8) & 0xFF;
                    buffer[position + 2] = timestampDelta & 0xFF;
                }
                position += 3;
            } else {
                buffer[0] |= 0xC0;
            }
            if (extendTimestamp) {
                buffer[position++] = (extendTimestamp >>> 24) & 0xFF;
                buffer[position++] = (extendTimestamp >>> 16) & 0xFF;
                buffer[position++] = (extendTimestamp >>> 8) & 0xFF;
                buffer[position++] = extendTimestamp & 0xFF;
            }
            chunkStream.sentTimestamp = timestamp;
            chunkStream.sentStreamId = message.streamId;
            chunkStream.sentTypeId = message.typeId;
            chunkStream.sentLength = messageLength;
            var sent = 0;
            while (sent < messageLength) {
                var currentChunkLength = Math.min(messageLength - sent, this.chunkSize);
                buffer.set(data.subarray(sent, sent + currentChunkLength), position);
                sent += currentChunkLength;
                this.ondata(buffer.subarray(0, position + currentChunkLength));
                buffer[0] |= 0xC0;
                position = chunkStreamIdSize;
            }
            return timestamp;
        }
        _getChunkStream(id) {
            var chunkStream = this.chunkStreams[id];
            if (!chunkStream) {
                this.chunkStreams[id] = chunkStream = new ChunkedStream(id);
                chunkStream.setBuffer(true);
                chunkStream.onmessage = function(message) {
                    if (this.onmessage) {
                        this.onmessage(message);
                    }
                }.bind(this);
            }
            return chunkStream;
        }
        _parseChunkedData() {
            if (this.bufferLength < 1) {
                return;
            }
            var chunkType = (this.buffer[0] >> 6) & 3;
            var chunkHeaderPosition = 1;
            var chunkStreamId = this.buffer[0] & 0x3F;
            if (chunkStreamId === 0) {
                if (this.bufferLength < 2) {
                    return;
                }
                chunkStreamId = this.buffer[1] + 64;
                chunkHeaderPosition = 2;
            } else if (chunkStreamId === 1) {
                if (this.bufferLength < 2) {
                    return;
                }
                chunkStreamId = (this.buffer[1] << 8) + this.buffer[2] + 64;
                chunkHeaderPosition = 3;
            }
            var chunkHeaderSize = chunkType === 0 ? 11 : chunkType === 1 ? 7 :
                chunkType === 2 ? 3 : 0;
            if (this.bufferLength < chunkHeaderPosition + chunkHeaderSize) {
                return;
            }
            var extendTimestampSize = chunkType !== 3 &&
                this.buffer[chunkHeaderPosition] === 0xFF &&
                this.buffer[chunkHeaderPosition + 1] === 0xFF &&
                this.buffer[chunkHeaderPosition + 2] === 0xFF ? 4 : 0;
            var totalChunkHeaderSize = chunkHeaderPosition + chunkHeaderSize +
                extendTimestampSize;
            if (this.bufferLength < totalChunkHeaderSize) {
                return;
            }
            var chunkStream = this._getChunkStream(chunkStreamId);
            var chunkTimestamp;
            if (chunkType === 3) {
                chunkTimestamp = chunkStream.lastTimestamp;
            } else {
                chunkTimestamp = (this.buffer[chunkHeaderPosition] << 16) |
                    (this.buffer[chunkHeaderPosition + 1] << 8) |
                    this.buffer[chunkHeaderPosition + 2];
            }
            if (extendTimestampSize) {
                var chunkTimestampPosition = chunkHeaderPosition + chunkHeaderSize;
                chunkTimestamp = (this.buffer[chunkTimestampPosition] << 24) |
                    (this.buffer[chunkTimestampPosition + 1] << 16) |
                    (this.buffer[chunkTimestampPosition + 2] << 8) |
                    this.buffer[chunkTimestampPosition + 3];
            }
            if (chunkType === 1 || chunkType === 2) {
                chunkTimestamp = (chunkStream.lastTimestamp + chunkTimestamp) | 0;
            }
            var messageLength = chunkStream.lastLength;
            var messageTypeId = chunkStream.lastTypeId;
            var messageStreamId = chunkStream.lastStreamId;
            if (chunkType === 0 || chunkType === 1) {
                messageLength = (this.buffer[chunkHeaderPosition + 3] << 16) |
                    (this.buffer[chunkHeaderPosition + 4] << 8) |
                    this.buffer[chunkHeaderPosition + 5];
                messageTypeId = this.buffer[chunkHeaderPosition + 6];
            }
            if (chunkType === 0) {
                messageStreamId = (this.buffer[chunkHeaderPosition + 10] << 24) |
                    (this.buffer[chunkHeaderPosition + 9] << 16) |
                    (this.buffer[chunkHeaderPosition + 8] << 8) |
                    this.buffer[chunkHeaderPosition + 7];
            }
            var read, tailLength, firstChunk;
            if (chunkType === 3 && chunkStream.waitingForBytes) {
                firstChunk = false;
                read = Math.min(chunkStream.waitingForBytes, this.peerChunkSize);
                tailLength = chunkStream.waitingForBytes - read;
            } else {
                firstChunk = true;
                read = Math.min(messageLength, this.peerChunkSize);
                tailLength = messageLength - read;
            }
            if (this.bufferLength < totalChunkHeaderSize + read) {
                return;
            }
            release || (!firstChunk && tailLength) ||
                console.log('Chunk received: cs:' + chunkStreamId + '; ' +
                    'f/l:' + firstChunk + '/' + (!tailLength) + ';  len:' + messageLength);
            chunkStream.lastTimestamp = chunkTimestamp;
            chunkStream.lastLength = messageLength;
            chunkStream.lastTypeId = messageTypeId;
            chunkStream.lastStreamId = messageStreamId;
            chunkStream.lastMessageComplete = !tailLength;
            chunkStream.waitingForBytes = tailLength;
            chunkStream._push(this.buffer.subarray(totalChunkHeaderSize, totalChunkHeaderSize + read), firstChunk, !tailLength);
            return totalChunkHeaderSize + read;
        }
        start() {
            this.epochStart = Date.now();
            this.ondata(new Uint8Array([PROTOCOL_VERSION]));
            this.randomData = new Uint8Array(RANDOM_DATA_SIZE);
            this.randomData[0] = 0;
            this.randomData[1] = 0;
            this.randomData[2] = 0;
            this.randomData[3] = 0;
            for (var i = 8; i < RANDOM_DATA_SIZE; i++) {
                this.randomData[i] = (Math.random() * 256) | 0;
            }
            this.ondata(this.randomData);
            console.log('## connected');
        }
        stop(error) {
            if (error) {
                console.error('socket error!!!');
            }
            console.log('## closed');
        }
        _fail(message) {
            console.error('failed: ' + message);
            this.state = 'failed';
            this.onclose();
            throw new Error(message);
        }
    }
    RtmpJs.ChunkedChannel = ChunkedChannel;
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var flash = Shumway.AVM2.AS.flash;
    var TRANSPORT_ENCODING = 0;
    var MAIN_CHUNKED_STREAM_ID = 3;
    var CONNECT_TRANSACTION_ID = 1;
    var DEFAULT_STREAM_ID = 0;
    var COMMAND_MESSAGE_AMF0_ID = 20;
    var COMMAND_MESSAGE_AMF3_ID = 17;
    var SET_BUFFER_CONTROL_MESSAGE_ID = 3;
    var PING_REQUEST_CONTROL_MESSAGE_ID = 6;
    var PING_RESPONSE_CONTROL_MESSAGE_ID = 7;
    class BaseTransport {
        constructor() {
            this._streams = [];
        }
        connect(properties, args) {
            throw new Error('Abstract BaseTransport.connect method');
        }
        _initChannel(properties, args) {
            var channel = new RtmpJs.ChunkedChannel();
            var transport = this;
            channel.oncreated = function() {
                var ba = new flash.utils.ByteArray();
                ba.objectEncoding = TRANSPORT_ENCODING;
                ba.writeObject('connect');
                ba.writeObject(CONNECT_TRANSACTION_ID);
                ba.writeObject(properties);
                ba.writeObject(args || null);
                release || console.log('.. Connect sent');
                channel.send(MAIN_CHUNKED_STREAM_ID, {
                    streamId: DEFAULT_STREAM_ID,
                    typeId: TRANSPORT_ENCODING ? COMMAND_MESSAGE_AMF3_ID : COMMAND_MESSAGE_AMF0_ID,
                    data: new Uint8Array(ba._buffer, 0, ba.length)
                });
            };
            channel.onmessage = function(message) {
                release || console.log('.. Data received: typeId:' + message.typeId +
                    ', streamId:' + message.streamId +
                    ', cs: ' + message.chunkedStreamId);
                if (message.streamId !== 0) {
                    transport._streams[message.streamId]._push(message);
                    return;
                }
                if (message.typeId === COMMAND_MESSAGE_AMF0_ID ||
                    message.typeId === COMMAND_MESSAGE_AMF3_ID) {
                    var ba = new flash.utils.ByteArray();
                    ba.writeRawBytes(message.data);
                    ba.position = 0;
                    ba.objectEncoding = message.typeId === COMMAND_MESSAGE_AMF0_ID ? 0 : 3;
                    var commandName = ba.readObject();
                    if (commandName === undefined) {
                        ba.objectEncoding = 0;
                        commandName = ba.readObject();
                    }
                    var transactionId = ba.readObject();
                    if (commandName === '_result' || commandName === '_error') {
                        var isError = commandName === '_error';
                        if (transactionId === CONNECT_TRANSACTION_ID) {
                            var properties = ba.readObject();
                            var information = ba.readObject();
                            if (transport.onconnected) {
                                transport.onconnected({ properties: properties, information: information, isError: isError });
                            }
                        } else {
                            var commandObject = ba.readObject();
                            var streamId = ba.readObject();
                            if (transport.onstreamcreated) {
                                var stream = new NetStream(transport, streamId);
                                transport._streams[streamId] = stream;
                                transport.onstreamcreated({ transactionId: transactionId, commandObject: commandObject, streamId: streamId, stream: stream, isError: isError });
                            }
                        }
                    } else if (commandName === 'onBWCheck' || commandName === 'onBWDone') {
                        transport.sendCommandOrResponse('_error', transactionId, null, { code: 'NetConnection.Call.Failed', level: 'error' });
                    } else {
                        var commandObject = ba.readObject();
                        var response = ba.position < ba.length ? ba.readObject() : undefined;
                        if (transport.onresponse) {
                            transport.onresponse({ commandName: commandName, transactionId: transactionId, commandObject: commandObject, response: response });
                        }
                    }
                    return;
                }
            };
            channel.onusercontrolmessage = function(e) {
                release || console.log('.. Event ' + e.type + ' +' + e.data.length + ' bytes');
                if (e.type === PING_REQUEST_CONTROL_MESSAGE_ID) {
                    channel.sendUserControlMessage(PING_RESPONSE_CONTROL_MESSAGE_ID, e.data);
                }
                if (transport.onevent) {
                    transport.onevent({ type: e.type, data: e.data });
                }
            };
            return (this.channel = channel);
        }
        call(procedureName, transactionId, commandObject, args) {
            var channel = this.channel;
            var ba = new flash.utils.ByteArray();
            ba.objectEncoding = TRANSPORT_ENCODING;
            ba.writeObject(procedureName);
            ba.writeObject(transactionId);
            ba.writeObject(commandObject);
            ba.writeObject(args);
            channel.send(MAIN_CHUNKED_STREAM_ID, {
                streamId: DEFAULT_STREAM_ID,
                typeId: TRANSPORT_ENCODING ? COMMAND_MESSAGE_AMF3_ID : COMMAND_MESSAGE_AMF0_ID,
                data: new Uint8Array(ba._buffer, 0, ba.length)
            });
        }
        createStream(transactionId, commandObject) {
            this.sendCommandOrResponse('createStream', transactionId, commandObject);
        }
        sendCommandOrResponse(commandName, transactionId, commandObject, response) {
            var channel = this.channel;
            var ba = new flash.utils.ByteArray();
            ba.writeByte(0);
            ba.objectEncoding = 0;
            ba.writeObject(commandName);
            ba.writeObject(transactionId);
            ba.writeObject(commandObject || null);
            if (arguments.length > 3) {
                ba.writeObject(response);
            }
            channel.send(MAIN_CHUNKED_STREAM_ID, {
                streamId: DEFAULT_STREAM_ID,
                typeId: COMMAND_MESSAGE_AMF3_ID,
                data: new Uint8Array(ba._buffer, 0, ba.length)
            });
        }
        _setBuffer(streamId, ms) {
            this.channel.sendUserControlMessage(SET_BUFFER_CONTROL_MESSAGE_ID, new Uint8Array([
                (streamId >> 24) & 0xFF,
                (streamId >> 16) & 0xFF,
                (streamId >> 8) & 0xFF,
                streamId & 0xFF,
                (ms >> 24) & 0xFF,
                (ms >> 16) & 0xFF,
                (ms >> 8) & 0xFF,
                ms & 0xFF
            ]));
        }
        _sendCommand(streamId, data) {
            this.channel.send(8, {
                streamId: streamId,
                typeId: TRANSPORT_ENCODING ? COMMAND_MESSAGE_AMF3_ID : COMMAND_MESSAGE_AMF0_ID,
                data: data
            });
        }
    }
    RtmpJs.BaseTransport = BaseTransport;
    //@NOTE: Pravin, we can change this
    var DEFAULT_BUFFER_LENGTH = 100;
    class NetStream {
        constructor(transport, streamId) {
            this.transport = transport;
            this.streamId = streamId;
        }
        play(name, start, duration, reset) {
            var ba = new flash.utils.ByteArray();
            ba.objectEncoding = TRANSPORT_ENCODING;
            ba.writeObject('play');
            ba.writeObject(0);
            ba.writeObject(null);
            ba.writeObject(name);
            if (arguments.length > 1) {
                ba.writeObject(start);
            }
            if (arguments.length > 2) {
                ba.writeObject(duration);
            }
            if (arguments.length > 3) {
                ba.writeObject(reset);
            }
            this.transport._sendCommand(this.streamId, new Uint8Array(ba._buffer, 0, ba.length));
            this.transport._setBuffer(this.streamId, DEFAULT_BUFFER_LENGTH);
        }
        _push(message) {
            switch (message.typeId) {
                case 8:
                case 9:
                    if (this.ondata) {
                        this.ondata(message);
                    }
                    break;
                case 18:
                case 20:
                    var args = [];
                    var ba = new flash.utils.ByteArray();
                    ba.writeRawBytes(message.data);
                    ba.position = 0;
                    ba.objectEncoding = 0;
                    while (ba.position < ba.length) {
                        args.push(ba.readObject());
                    }
                    if (message.typeId === 18 && this.onscriptdata) {
                        this.onscriptdata.apply(this, args);
                    }
                    if (message.typeId === 20 && this.oncallback) {
                        this.oncallback.apply(this, args);
                    }
                    break;
            }
        }
    }

    function parseConnectionString(s) {
        var protocolSeparatorIndex = s.indexOf(':');
        if (protocolSeparatorIndex < 0) {
            return null;
        }
        if (s[protocolSeparatorIndex + 1] !== '/') {
            return null;
        }
        var protocol = s.substring(0, protocolSeparatorIndex).toLocaleLowerCase();
        if (protocol !== 'rtmp' && protocol !== 'rtmpt' && protocol !== 'rtmps' &&
            protocol !== 'rtmpe' && protocol !== 'rtmpte' && protocol !== 'rtmfp') {
            return null;
        }
        var host, port;
        var appnameSeparator = protocolSeparatorIndex + 1;
        if (s[protocolSeparatorIndex + 2] === '/') {
            appnameSeparator = s.indexOf('/', protocolSeparatorIndex + 3);
            if (appnameSeparator < 0) {
                return undefined;
            }
            var portSeparator = s.indexOf(':', protocolSeparatorIndex + 1);
            if (portSeparator >= 0 && portSeparator < appnameSeparator) {
                host = s.substring(protocolSeparatorIndex + 3, portSeparator);
                port = +s.substring(portSeparator + 1, appnameSeparator);
            } else {
                host = s.substring(protocolSeparatorIndex + 3, appnameSeparator);
            }
        }
        var app = s.substring(appnameSeparator + 1);
        return {
            protocol: protocol,
            host: host,
            port: port,
            app: app
        };
    }
    RtmpJs.parseConnectionString = parseConnectionString;
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var Browser;
    (function(Browser) {
        var COMBINE_RTMPT_DATA = true;
        class RtmptTransport extends RtmpJs.BaseTransport {
            constructor(connectionSettings) {
                super();
                var host = connectionSettings.host || 'localhost';
                var url = (connectionSettings.ssl ? 'https' : 'http') + '://' + host;
                if (connectionSettings.port) {
                    url += ':' + connectionSettings.port;
                }
                this.baseUrl = url;
                this.stopped = false;
                this.sessionId = null;
                this.requestId = 0;
                this.data = [];
            }
            connect(properties, args) {
                var channel = this._initChannel(properties, args);
                channel.ondata = function(data) {
                    release || console.log('Bytes written: ' + data.length);
                    this.data.push(new Uint8Array(data));
                }.bind(this);
                channel.onclose = function() {
                    this.stopped = true;
                }.bind(this);
                post(this.baseUrl + '/fcs/ident2', null, function(data, status) {
                    if (status !== 404) {
                        throw new Error('Unexpected response: ' + status);
                    }
                    post(this.baseUrl + '/open/1', null, function(data, status) {
                        this.sessionId = String.fromCharCode.apply(null, data).slice(0, -1);
                        console.log('session id: ' + this.sessionId);
                        this.tick();
                        channel.start();
                    }.bind(this));
                }.bind(this));
            }
            tick() {
                var continueSend = function(data, status) {
                    if (status !== 200) {
                        throw new Error('Invalid HTTP status');
                    }
                    var idle = data[0];
                    if (data.length > 1) {
                        this.channel.push(data.subarray(1));
                    }
                    setTimeout(this.tick.bind(this), idle * 16);
                }.bind(this);
                if (this.stopped) {
                    post(this.baseUrl + '/close/2', null, function() {});
                    return;
                }
                if (this.data.length > 0) {
                    var data;
                    if (COMBINE_RTMPT_DATA) {
                        var length = 0;
                        this.data.forEach(function(i) {
                            length += i.length;
                        });
                        var pos = 0;
                        data = new Uint8Array(length);
                        this.data.forEach(function(i) {
                            data.set(i, pos);
                            pos += i.length;
                        });
                        this.data.length = 0;
                    } else {
                        data = this.data.shift();
                    }
                    post(this.baseUrl + '/send/' + this.sessionId + '/' + (this.requestId++), data, continueSend);
                } else {
                    post(this.baseUrl + '/idle/' + this.sessionId + '/' + (this.requestId++), null, continueSend);
                }
            }
        }
        Browser.RtmptTransport = RtmptTransport;
        var emptyPostData = new Uint8Array([0]);

        function post(path, data, onload) {
            data || (data = emptyPostData);
            var createRtmpXHR = window.createRtmpXHR;
            var xhr = createRtmpXHR ? createRtmpXHR() : new XMLHttpRequest();
            xhr.open('POST', path, true);
            xhr.responseType = 'arraybuffer';
            xhr.setRequestHeader('Content-Type', 'application/x-fcs');
            xhr.onload = function(e) {
                onload(new Uint8Array(xhr.response), xhr.status);
            };
            xhr.onerror = function(e) {
                console.log('error');
                throw new Error('HTTP error');
            };
            xhr.send(data);
        }
    })(Browser = RtmpJs.Browser || (RtmpJs.Browser = {}));
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var Browser;
    (function(Browser) {
        class RtmpWsTransport extends RtmpJs.BaseTransport {
            constructor(connectionSettings) {
                super();
                if (typeof connectionSettings === 'string') {
                    connectionSettings = { url: connectionSettings };
                }
                this.wsurl = connectionSettings.url;
            }
            connect(onClosed, properties, args) {
                var channel = this._initChannel(properties, args);
                var socketError = false;
                var socket = new WebSocket(this.wsurl);
                this.socket = socket;
                socket.binaryType = 'arraybuffer';
                release || console.log('Opening binary websocket connection: ' + this.wsurl);
                socket.onopen = function() {
                    channel.ondata = function(data) {
                        var buf = new Uint8Array(data);
                        release || console.log('Bytes written: ' + buf.length);
                        socket.send(buf.buffer);
                    };
                    channel.onclose = function() {
                        socket.close();
                    };
                    channel.start();
                };
                socket.onclose = function(e) {
                    console.log("socket.onclose called");
                    channel.stop(socketError);
                    onClosed();
                };
                socket.onerror = function(e) {
                    socketError = true;
                    release || console.error('WebSocket error: ', e);
                };
                socket.onmessage = function(e) {
                    release || console.log('Bytes read: ', e.data.byteLength);
                    channel.push(new Uint8Array(e.data));
                };
            }
            close() {
                this.socket.close();
            }
        }
        Browser.RtmpWsTransport = RtmpWsTransport;
    })(Browser = RtmpJs.Browser || (RtmpJs.Browser = {}));
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    class MSEBufferWriter {
        constructor(mediaSource, dataSource) {
            this.mediaSource = mediaSource;
            this.dataSource = dataSource;
            this.dataSource.onData = this.pushData.bind(this);
            this.updateEnabled = false;
            this.buffer = [];
            this.sourceBuffer = null;
            this.sourceBufferUpdatedBound = null;
        }
        allowWriting() {
            this.updateEnabled = true;
            this.update();
        }
        pushData(data) {
            this.buffer.push(data);
            this.update();
        }
        update() {
            if (!this.updateEnabled || this.buffer.length === 0) {
                return;
            }
            if (!this.sourceBuffer) {
                this.sourceBuffer = this.mediaSource.addSourceBuffer(this.dataSource.mimeType);
                this.sourceBufferUpdatedBound = this._sourceBufferUpdated.bind(this);
                this.sourceBuffer.addEventListener('update', this.sourceBufferUpdatedBound);
            }
            this.updateEnabled = false;
            var data = this.buffer.shift();
            if (data === null) {
                this.sourceBuffer.removeEventListener('update', this.sourceBufferUpdatedBound);
                return;
            }
            this.sourceBuffer.appendBuffer(data);
        }
        _sourceBufferUpdated(e) {
            this.updateEnabled = true;
            this.update();
        }
        finish() {
            this.buffer.push(null);
            this.update();
        }
    }
    class MSEWriter {
        constructor(mediaSource) {
            this.bufferWriters = [];
            this.mediaSource = mediaSource;
            this.mediaSourceOpened = false;
            this.mediaSource.addEventListener('sourceopen', function(e) {
                this.mediaSourceOpened = true;
                this.bufferWriters.forEach(function(writer) {
                    writer.allowWriting();
                });
            }.bind(this));
            this.mediaSource.addEventListener('sourceend', function(e) {
                this.mediaSourceOpened = false;
            }.bind(this));
        }
        listen(dataSource) {
            var writer = new MSEBufferWriter(this.mediaSource, dataSource);
            this.bufferWriters.push(writer);
            if (this.mediaSourceOpened) {
                writer.allowWriting();
            }
        }
    }
    RtmpJs.MSEWriter = MSEWriter;
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var MP4;
    (function(MP4) {
        var Iso;
        (function(Iso) {
            var utf8decode = Shumway.StringUtilities.utf8decode;
            var START_DATE = -2082844800000;
            var DEFAULT_MOVIE_MATRIX = [1.0, 0, 0, 0, 1.0, 0, 0, 0, 1.0];
            var DEFAULT_OP_COLOR = [0, 0, 0];

            function concatArrays(arg0, ...args) {
                return Array.prototype.concat.apply(arg0, args);
            }

            function writeInt32(data, offset, value) {
                data[offset] = (value >> 24) & 255;
                data[offset + 1] = (value >> 16) & 255;
                data[offset + 2] = (value >> 8) & 255;
                data[offset + 3] = value & 255;
            }

            function decodeInt32(s) {
                return (s.charCodeAt(0) << 24) | (s.charCodeAt(1) << 16) |
                    (s.charCodeAt(2) << 8) | s.charCodeAt(3);
            }

            function encodeDate(d) {
                return ((d - START_DATE) / 1000) | 0;
            }

            function encodeFloat_16_16(f) {
                return (f * 0x10000) | 0;
            }

            function encodeFloat_2_30(f) {
                return (f * 0x40000000) | 0;
            }

            function encodeFloat_8_8(f) {
                return (f * 0x100) | 0;
            }

            function encodeLang(s) {
                return ((s.charCodeAt(0) & 0x1F) << 10) | ((s.charCodeAt(1) & 0x1F) << 5) | (s.charCodeAt(2) & 0x1F);
            }
            class Box {
                constructor(boxtype, extendedType) {
                    this.boxtype = boxtype;
                    if (boxtype === 'uuid') {
                        this.userType = extendedType;
                    }
                }
                layout(offset) {
                    this.offset = offset;
                    var size = 8;
                    if (this.userType) {
                        size += 16;
                    }
                    this.size = size;
                    return size;
                }
                write(data) {
                    writeInt32(data, this.offset, this.size);
                    writeInt32(data, this.offset + 4, decodeInt32(this.boxtype));
                    if (!this.userType) {
                        return 8;
                    }
                    data.set(this.userType, this.offset + 8);
                    return 24;
                }
                toUint8Array() {
                    var size = this.layout(0);
                    var data = new Uint8Array(size);
                    this.write(data);
                    return data;
                }
            }
            Iso.Box = Box;
            class FullBox extends Box {
                constructor(boxtype, version = 0, flags = 0) {
                    super(boxtype);
                    this.version = version;
                    this.flags = flags;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, (this.version << 24) | this.flags);
                    return offset + 4;
                }
            }
            Iso.FullBox = FullBox;
            class FileTypeBox extends Box {
                constructor(majorBrand, minorVersion, compatibleBrands) {
                    super('ftype');
                    this.majorBrand = majorBrand;
                    this.minorVersion = minorVersion;
                    this.compatibleBrands = compatibleBrands;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 4 * (2 + this.compatibleBrands.length);
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, decodeInt32(this.majorBrand));
                    writeInt32(data, this.offset + offset + 4, this.minorVersion);
                    offset += 8;
                    this.compatibleBrands.forEach((brand) => {
                        writeInt32(data, this.offset + offset, decodeInt32(brand));
                        offset += 4;
                    }, this);
                    return offset;
                }
            }
            Iso.FileTypeBox = FileTypeBox;
            class BoxContainerBox extends Box {
                constructor(type, children) {
                    super(type);
                    this.children = children;
                }
                layout(offset) {
                    var size = super.layout(offset);
                    this.children.forEach((child) => {
                        if (!child) {
                            return;
                        }
                        size += child.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    this.children.forEach((child) => {
                        if (!child) {
                            return;
                        }
                        offset += child.write(data);
                    });
                    return offset;
                }
            }
            Iso.BoxContainerBox = BoxContainerBox;
            class MovieBox extends BoxContainerBox {
                constructor(header, tracks, extendsBox, userData) {
                    super('moov', concatArrays([header], tracks, [extendsBox, userData]));
                    this.header = header;
                    this.tracks = tracks;
                    this.extendsBox = extendsBox;
                    this.userData = userData;
                }
            }
            Iso.MovieBox = MovieBox;
            class MovieHeaderBox extends FullBox {
                constructor(timescale, duration, nextTrackId, rate = 1.0, volume = 1.0, matrix = DEFAULT_MOVIE_MATRIX, creationTime = START_DATE, modificationTime = START_DATE) {
                    super('mvhd', 0, 0);
                    this.timescale = timescale;
                    this.duration = duration;
                    this.nextTrackId = nextTrackId;
                    this.rate = rate;
                    this.volume = volume;
                    this.matrix = matrix;
                    this.creationTime = creationTime;
                    this.modificationTime = modificationTime;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 16 + 4 + 2 + 2 + 8 + 36 + 24 + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, encodeDate(this.creationTime));
                    writeInt32(data, this.offset + offset + 4, encodeDate(this.modificationTime));
                    writeInt32(data, this.offset + offset + 8, this.timescale);
                    writeInt32(data, this.offset + offset + 12, this.duration);
                    offset += 16;
                    writeInt32(data, this.offset + offset, encodeFloat_16_16(this.rate));
                    writeInt32(data, this.offset + offset + 4, encodeFloat_8_8(this.volume) << 16);
                    writeInt32(data, this.offset + offset + 8, 0);
                    writeInt32(data, this.offset + offset + 12, 0);
                    offset += 16;
                    writeInt32(data, this.offset + offset, encodeFloat_16_16(this.matrix[0]));
                    writeInt32(data, this.offset + offset + 4, encodeFloat_16_16(this.matrix[1]));
                    writeInt32(data, this.offset + offset + 8, encodeFloat_16_16(this.matrix[2]));
                    writeInt32(data, this.offset + offset + 12, encodeFloat_16_16(this.matrix[3]));
                    writeInt32(data, this.offset + offset + 16, encodeFloat_16_16(this.matrix[4]));
                    writeInt32(data, this.offset + offset + 20, encodeFloat_16_16(this.matrix[5]));
                    writeInt32(data, this.offset + offset + 24, encodeFloat_2_30(this.matrix[6]));
                    writeInt32(data, this.offset + offset + 28, encodeFloat_2_30(this.matrix[7]));
                    writeInt32(data, this.offset + offset + 32, encodeFloat_2_30(this.matrix[8]));
                    offset += 36;
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, 0);
                    writeInt32(data, this.offset + offset + 8, 0);
                    writeInt32(data, this.offset + offset + 12, 0);
                    writeInt32(data, this.offset + offset + 16, 0);
                    writeInt32(data, this.offset + offset + 20, 0);
                    offset += 24;
                    writeInt32(data, this.offset + offset, this.nextTrackId);
                    offset += 4;
                    return offset;
                }
            }
            Iso.MovieHeaderBox = MovieHeaderBox;
            let TrackHeaderFlags;
            (function(TrackHeaderFlags) {
                TrackHeaderFlags[TrackHeaderFlags["TRACK_ENABLED"] = 1] = "TRACK_ENABLED";
                TrackHeaderFlags[TrackHeaderFlags["TRACK_IN_MOVIE"] = 2] = "TRACK_IN_MOVIE";
                TrackHeaderFlags[TrackHeaderFlags["TRACK_IN_PREVIEW"] = 4] = "TRACK_IN_PREVIEW";
            })(TrackHeaderFlags = Iso.TrackHeaderFlags || (Iso.TrackHeaderFlags = {}));
            class TrackHeaderBox extends FullBox {
                constructor(flags, trackId, duration, width, height, volume, alternateGroup = 0, layer = 0, matrix = DEFAULT_MOVIE_MATRIX, creationTime = START_DATE, modificationTime = START_DATE) {
                    super('tkhd', 0, flags);
                    this.trackId = trackId;
                    this.duration = duration;
                    this.width = width;
                    this.height = height;
                    this.volume = volume;
                    this.alternateGroup = alternateGroup;
                    this.layer = layer;
                    this.matrix = matrix;
                    this.creationTime = creationTime;
                    this.modificationTime = modificationTime;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 20 + 8 + 6 + 2 + 36 + 8;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, encodeDate(this.creationTime));
                    writeInt32(data, this.offset + offset + 4, encodeDate(this.modificationTime));
                    writeInt32(data, this.offset + offset + 8, this.trackId);
                    writeInt32(data, this.offset + offset + 12, 0);
                    writeInt32(data, this.offset + offset + 16, this.duration);
                    offset += 20;
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, 0);
                    writeInt32(data, this.offset + offset + 8, (this.layer << 16) | this.alternateGroup);
                    writeInt32(data, this.offset + offset + 12, encodeFloat_8_8(this.volume) << 16);
                    offset += 16;
                    writeInt32(data, this.offset + offset, encodeFloat_16_16(this.matrix[0]));
                    writeInt32(data, this.offset + offset + 4, encodeFloat_16_16(this.matrix[1]));
                    writeInt32(data, this.offset + offset + 8, encodeFloat_16_16(this.matrix[2]));
                    writeInt32(data, this.offset + offset + 12, encodeFloat_16_16(this.matrix[3]));
                    writeInt32(data, this.offset + offset + 16, encodeFloat_16_16(this.matrix[4]));
                    writeInt32(data, this.offset + offset + 20, encodeFloat_16_16(this.matrix[5]));
                    writeInt32(data, this.offset + offset + 24, encodeFloat_2_30(this.matrix[6]));
                    writeInt32(data, this.offset + offset + 28, encodeFloat_2_30(this.matrix[7]));
                    writeInt32(data, this.offset + offset + 32, encodeFloat_2_30(this.matrix[8]));
                    offset += 36;
                    writeInt32(data, this.offset + offset, encodeFloat_16_16(this.width));
                    writeInt32(data, this.offset + offset + 4, encodeFloat_16_16(this.height));
                    offset += 8;
                    return offset;
                }
            }
            Iso.TrackHeaderBox = TrackHeaderBox;
            class MediaHeaderBox extends FullBox {
                constructor(timescale, duration, language = 'unk', creationTime = START_DATE, modificationTime = START_DATE) {
                    super('mdhd', 0, 0);
                    this.timescale = timescale;
                    this.duration = duration;
                    this.language = language;
                    this.creationTime = creationTime;
                    this.modificationTime = modificationTime;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 16 + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, encodeDate(this.creationTime));
                    writeInt32(data, this.offset + offset + 4, encodeDate(this.modificationTime));
                    writeInt32(data, this.offset + offset + 8, this.timescale);
                    writeInt32(data, this.offset + offset + 12, this.duration);
                    writeInt32(data, this.offset + offset + 16, encodeLang(this.language) << 16);
                    return offset + 20;
                }
            }
            Iso.MediaHeaderBox = MediaHeaderBox;
            class HandlerBox extends FullBox {
                constructor(handlerType, name) {
                    super('hdlr', 0, 0);
                    this.handlerType = handlerType;
                    this.name = name;
                    this._encodedName = utf8decode(this.name);
                }
                layout(offset) {
                    this.size = super.layout(offset) + 8 + 12 + (this._encodedName.length + 1);
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, decodeInt32(this.handlerType));
                    writeInt32(data, this.offset + offset + 8, 0);
                    writeInt32(data, this.offset + offset + 12, 0);
                    writeInt32(data, this.offset + offset + 16, 0);
                    offset += 20;
                    data.set(this._encodedName, this.offset + offset);
                    data[this.offset + offset + this._encodedName.length] = 0;
                    offset += this._encodedName.length + 1;
                    return offset;
                }
            }
            Iso.HandlerBox = HandlerBox;
            class SoundMediaHeaderBox extends FullBox {
                constructor(balance = 0.0) {
                    super('smhd', 0, 0);
                    this.balance = balance;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, encodeFloat_8_8(this.balance) << 16);
                    return offset + 4;
                }
            }
            Iso.SoundMediaHeaderBox = SoundMediaHeaderBox;
            class VideoMediaHeaderBox extends FullBox {
                constructor(graphicsMode = 0, opColor = DEFAULT_OP_COLOR) {
                    super('vmhd', 0, 0);
                    this.graphicsMode = graphicsMode;
                    this.opColor = opColor;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 8;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, (this.graphicsMode << 16) | this.opColor[0]);
                    writeInt32(data, this.offset + offset + 4, (this.opColor[1] << 16) | this.opColor[2]);
                    return offset + 8;
                }
            }
            Iso.VideoMediaHeaderBox = VideoMediaHeaderBox;
            Iso.SELF_CONTAINED_DATA_REFERENCE_FLAG = 0x000001;
            class DataEntryUrlBox extends FullBox {
                constructor(flags, location = null) {
                    super('url ', 0, flags);
                    this.location = location;
                    if (!(flags & Iso.SELF_CONTAINED_DATA_REFERENCE_FLAG)) {
                        this._encodedLocation = utf8decode(location);
                    }
                }
                layout(offset) {
                    var size = super.layout(offset);
                    if (this._encodedLocation) {
                        size += this._encodedLocation.length + 1;
                    }
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    if (this._encodedLocation) {
                        data.set(this._encodedLocation, this.offset + offset);
                        data[this.offset + offset + this._encodedLocation.length] = 0;
                        offset += this._encodedLocation.length;
                    }
                    return offset;
                }
            }
            Iso.DataEntryUrlBox = DataEntryUrlBox;
            class DataReferenceBox extends FullBox {
                constructor(entries) {
                    super('dref', 0, 0);
                    this.entries = entries;
                }
                layout(offset) {
                    var size = super.layout(offset) + 4;
                    this.entries.forEach((entry) => {
                        size += entry.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, this.entries.length);
                    this.entries.forEach((entry) => {
                        offset += entry.write(data);
                    });
                    return offset;
                }
            }
            Iso.DataReferenceBox = DataReferenceBox;
            class DataInformationBox extends BoxContainerBox {
                constructor(dataReference) {
                    super('dinf', [dataReference]);
                    this.dataReference = dataReference;
                }
            }
            Iso.DataInformationBox = DataInformationBox;
            class SampleDescriptionBox extends FullBox {
                constructor(entries) {
                    super('stsd', 0, 0);
                    this.entries = entries;
                }
                layout(offset) {
                    var size = super.layout(offset);
                    size += 4;
                    this.entries.forEach((entry) => {
                        size += entry.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, this.entries.length);
                    offset += 4;
                    this.entries.forEach((entry) => {
                        offset += entry.write(data);
                    });
                    return offset;
                }
            }
            Iso.SampleDescriptionBox = SampleDescriptionBox;
            class SampleTableBox extends BoxContainerBox {
                constructor(sampleDescriptions, timeToSample, sampleToChunk, sampleSizes, chunkOffset) {
                    super('stbl', [sampleDescriptions, timeToSample, sampleToChunk, sampleSizes, chunkOffset]);
                    this.sampleDescriptions = sampleDescriptions;
                    this.timeToSample = timeToSample;
                    this.sampleToChunk = sampleToChunk;
                    this.sampleSizes = sampleSizes;
                    this.chunkOffset = chunkOffset;
                }
            }
            Iso.SampleTableBox = SampleTableBox;
            class MediaInformationBox extends BoxContainerBox {
                constructor(header, info, sampleTable) {
                    super('minf', [header, info, sampleTable]);
                    this.header = header;
                    this.info = info;
                    this.sampleTable = sampleTable;
                }
            }
            Iso.MediaInformationBox = MediaInformationBox;
            class MediaBox extends BoxContainerBox {
                constructor(header, handler, info) {
                    super('mdia', [header, handler, info]);
                    this.header = header;
                    this.handler = handler;
                    this.info = info;
                }
            }
            Iso.MediaBox = MediaBox;
            class TrackBox extends BoxContainerBox {
                constructor(header, media) {
                    super('trak', [header, media]);
                    this.header = header;
                    this.media = media;
                }
            }
            Iso.TrackBox = TrackBox;
            class TrackExtendsBox extends FullBox {
                constructor(trackId, defaultSampleDescriptionIndex, defaultSampleDuration, defaultSampleSize, defaultSampleFlags) {
                    super('trex', 0, 0);
                    this.trackId = trackId;
                    this.defaultSampleDescriptionIndex = defaultSampleDescriptionIndex;
                    this.defaultSampleDuration = defaultSampleDuration;
                    this.defaultSampleSize = defaultSampleSize;
                    this.defaultSampleFlags = defaultSampleFlags;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 20;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, this.trackId);
                    writeInt32(data, this.offset + offset + 4, this.defaultSampleDescriptionIndex);
                    writeInt32(data, this.offset + offset + 8, this.defaultSampleDuration);
                    writeInt32(data, this.offset + offset + 12, this.defaultSampleSize);
                    writeInt32(data, this.offset + offset + 16, this.defaultSampleFlags);
                    return offset + 20;
                }
            }
            Iso.TrackExtendsBox = TrackExtendsBox;
            class MovieExtendsBox extends BoxContainerBox {
                constructor(header, tracDefaults, levels) {
                    super('mvex', concatArrays([header], tracDefaults, [levels]));
                    this.header = header;
                    this.tracDefaults = tracDefaults;
                    this.levels = levels;
                }
            }
            Iso.MovieExtendsBox = MovieExtendsBox;
            class MetaBox extends FullBox {
                constructor(handler, otherBoxes) {
                    super('meta', 0, 0);
                    this.handler = handler;
                    this.otherBoxes = otherBoxes;
                }
                layout(offset) {
                    var size = super.layout(offset);
                    size += this.handler.layout(offset + size);
                    this.otherBoxes.forEach((box) => {
                        size += box.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    offset += this.handler.write(data);
                    this.otherBoxes.forEach((box) => {
                        offset += box.write(data);
                    });
                    return offset;
                }
            }
            Iso.MetaBox = MetaBox;
            class MovieFragmentHeaderBox extends FullBox {
                constructor(sequenceNumber) {
                    super('mfhd', 0, 0);
                    this.sequenceNumber = sequenceNumber;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, this.sequenceNumber);
                    return offset + 4;
                }
            }
            Iso.MovieFragmentHeaderBox = MovieFragmentHeaderBox;
            let TrackFragmentFlags;
            (function(TrackFragmentFlags) {
                TrackFragmentFlags[TrackFragmentFlags["BASE_DATA_OFFSET_PRESENT"] = 1] = "BASE_DATA_OFFSET_PRESENT";
                TrackFragmentFlags[TrackFragmentFlags["SAMPLE_DESCRIPTION_INDEX_PRESENT"] = 2] = "SAMPLE_DESCRIPTION_INDEX_PRESENT";
                TrackFragmentFlags[TrackFragmentFlags["DEFAULT_SAMPLE_DURATION_PRESENT"] = 8] = "DEFAULT_SAMPLE_DURATION_PRESENT";
                TrackFragmentFlags[TrackFragmentFlags["DEFAULT_SAMPLE_SIZE_PRESENT"] = 16] = "DEFAULT_SAMPLE_SIZE_PRESENT";
                TrackFragmentFlags[TrackFragmentFlags["DEFAULT_SAMPLE_FLAGS_PRESENT"] = 32] = "DEFAULT_SAMPLE_FLAGS_PRESENT";
            })(TrackFragmentFlags = Iso.TrackFragmentFlags || (Iso.TrackFragmentFlags = {}));
            class TrackFragmentHeaderBox extends FullBox {
                constructor(flags, trackId, baseDataOffset, sampleDescriptionIndex, defaultSampleDuration, defaultSampleSize, defaultSampleFlags) {
                    super('tfhd', 0, flags);
                    this.trackId = trackId;
                    this.baseDataOffset = baseDataOffset;
                    this.sampleDescriptionIndex = sampleDescriptionIndex;
                    this.defaultSampleDuration = defaultSampleDuration;
                    this.defaultSampleSize = defaultSampleSize;
                    this.defaultSampleFlags = defaultSampleFlags;
                }
                layout(offset) {
                    var size = super.layout(offset) + 4;
                    var flags = this.flags;
                    if (!!(flags & TrackFragmentFlags.BASE_DATA_OFFSET_PRESENT)) {
                        size += 8;
                    }
                    if (!!(flags & TrackFragmentFlags.SAMPLE_DESCRIPTION_INDEX_PRESENT)) {
                        size += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_DURATION_PRESENT)) {
                        size += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_SIZE_PRESENT)) {
                        size += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_FLAGS_PRESENT)) {
                        size += 4;
                    }
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    var flags = this.flags;
                    writeInt32(data, this.offset + offset, this.trackId);
                    offset += 4;
                    if (!!(flags & TrackFragmentFlags.BASE_DATA_OFFSET_PRESENT)) {
                        writeInt32(data, this.offset + offset, 0);
                        writeInt32(data, this.offset + offset + 4, this.baseDataOffset);
                        offset += 8;
                    }
                    if (!!(flags & TrackFragmentFlags.SAMPLE_DESCRIPTION_INDEX_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.sampleDescriptionIndex);
                        offset += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_DURATION_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.defaultSampleDuration);
                        offset += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_SIZE_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.defaultSampleSize);
                        offset += 4;
                    }
                    if (!!(flags & TrackFragmentFlags.DEFAULT_SAMPLE_FLAGS_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.defaultSampleFlags);
                        offset += 4;
                    }
                    return offset;
                }
            }
            Iso.TrackFragmentHeaderBox = TrackFragmentHeaderBox;
            class TrackFragmentBaseMediaDecodeTimeBox extends FullBox {
                constructor(baseMediaDecodeTime) {
                    super('tfdt', 0, 0);
                    this.baseMediaDecodeTime = baseMediaDecodeTime;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 4;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, this.baseMediaDecodeTime);
                    return offset + 4;
                }
            }
            Iso.TrackFragmentBaseMediaDecodeTimeBox = TrackFragmentBaseMediaDecodeTimeBox;
            class TrackFragmentBox extends BoxContainerBox {
                constructor(header, decodeTime, run) {
                    super('traf', [header, decodeTime, run]);
                    this.header = header;
                    this.decodeTime = decodeTime;
                    this.run = run;
                }
            }
            Iso.TrackFragmentBox = TrackFragmentBox;
            let SampleFlags;
            (function(SampleFlags) {
                SampleFlags[SampleFlags["IS_LEADING_MASK"] = 201326592] = "IS_LEADING_MASK";
                SampleFlags[SampleFlags["SAMPLE_DEPENDS_ON_MASK"] = 50331648] = "SAMPLE_DEPENDS_ON_MASK";
                SampleFlags[SampleFlags["SAMPLE_DEPENDS_ON_OTHER"] = 16777216] = "SAMPLE_DEPENDS_ON_OTHER";
                SampleFlags[SampleFlags["SAMPLE_DEPENDS_ON_NO_OTHERS"] = 33554432] = "SAMPLE_DEPENDS_ON_NO_OTHERS";
                SampleFlags[SampleFlags["SAMPLE_IS_DEPENDED_ON_MASK"] = 12582912] = "SAMPLE_IS_DEPENDED_ON_MASK";
                SampleFlags[SampleFlags["SAMPLE_HAS_REDUNDANCY_MASK"] = 3145728] = "SAMPLE_HAS_REDUNDANCY_MASK";
                SampleFlags[SampleFlags["SAMPLE_PADDING_VALUE_MASK"] = 917504] = "SAMPLE_PADDING_VALUE_MASK";
                SampleFlags[SampleFlags["SAMPLE_IS_NOT_SYNC"] = 65536] = "SAMPLE_IS_NOT_SYNC";
                SampleFlags[SampleFlags["SAMPLE_DEGRADATION_PRIORITY_MASK"] = 65535] = "SAMPLE_DEGRADATION_PRIORITY_MASK";
            })(SampleFlags = Iso.SampleFlags || (Iso.SampleFlags = {}));
            let TrackRunFlags;
            (function(TrackRunFlags) {
                TrackRunFlags[TrackRunFlags["DATA_OFFSET_PRESENT"] = 1] = "DATA_OFFSET_PRESENT";
                TrackRunFlags[TrackRunFlags["FIRST_SAMPLE_FLAGS_PRESENT"] = 4] = "FIRST_SAMPLE_FLAGS_PRESENT";
                TrackRunFlags[TrackRunFlags["SAMPLE_DURATION_PRESENT"] = 256] = "SAMPLE_DURATION_PRESENT";
                TrackRunFlags[TrackRunFlags["SAMPLE_SIZE_PRESENT"] = 512] = "SAMPLE_SIZE_PRESENT";
                TrackRunFlags[TrackRunFlags["SAMPLE_FLAGS_PRESENT"] = 1024] = "SAMPLE_FLAGS_PRESENT";
                TrackRunFlags[TrackRunFlags["SAMPLE_COMPOSITION_TIME_OFFSET"] = 2048] = "SAMPLE_COMPOSITION_TIME_OFFSET";
            })(TrackRunFlags = Iso.TrackRunFlags || (Iso.TrackRunFlags = {}));
            class TrackRunBox extends FullBox {
                constructor(flags, samples, dataOffset, firstSampleFlags) {
                    super('trun', 1, flags);
                    this.samples = samples;
                    this.dataOffset = dataOffset;
                    this.firstSampleFlags = firstSampleFlags;
                }
                layout(offset) {
                    var size = super.layout(offset) + 4;
                    var samplesCount = this.samples.length;
                    var flags = this.flags;
                    if (!!(flags & TrackRunFlags.DATA_OFFSET_PRESENT)) {
                        size += 4;
                    }
                    if (!!(flags & TrackRunFlags.FIRST_SAMPLE_FLAGS_PRESENT)) {
                        size += 4;
                    }
                    if (!!(flags & TrackRunFlags.SAMPLE_DURATION_PRESENT)) {
                        size += 4 * samplesCount;
                    }
                    if (!!(flags & TrackRunFlags.SAMPLE_SIZE_PRESENT)) {
                        size += 4 * samplesCount;
                    }
                    if (!!(flags & TrackRunFlags.SAMPLE_FLAGS_PRESENT)) {
                        size += 4 * samplesCount;
                    }
                    if (!!(flags & TrackRunFlags.SAMPLE_COMPOSITION_TIME_OFFSET)) {
                        size += 4 * samplesCount;
                    }
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    var samplesCount = this.samples.length;
                    var flags = this.flags;
                    writeInt32(data, this.offset + offset, samplesCount);
                    offset += 4;
                    if (!!(flags & TrackRunFlags.DATA_OFFSET_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.dataOffset);
                        offset += 4;
                    }
                    if (!!(flags & TrackRunFlags.FIRST_SAMPLE_FLAGS_PRESENT)) {
                        writeInt32(data, this.offset + offset, this.firstSampleFlags);
                        offset += 4;
                    }
                    for (var i = 0; i < samplesCount; i++) {
                        var sample = this.samples[i];
                        if (!!(flags & TrackRunFlags.SAMPLE_DURATION_PRESENT)) {
                            writeInt32(data, this.offset + offset, sample.duration);
                            offset += 4;
                        }
                        if (!!(flags & TrackRunFlags.SAMPLE_SIZE_PRESENT)) {
                            writeInt32(data, this.offset + offset, sample.size);
                            offset += 4;
                        }
                        if (!!(flags & TrackRunFlags.SAMPLE_FLAGS_PRESENT)) {
                            writeInt32(data, this.offset + offset, sample.flags);
                            offset += 4;
                        }
                        if (!!(flags & TrackRunFlags.SAMPLE_COMPOSITION_TIME_OFFSET)) {
                            writeInt32(data, this.offset + offset, sample.compositionTimeOffset);
                            offset += 4;
                        }
                    }
                    return offset;
                }
            }
            Iso.TrackRunBox = TrackRunBox;
            class MovieFragmentBox extends BoxContainerBox {
                constructor(header, trafs) {
                    super('moof', concatArrays([header], trafs));
                    this.header = header;
                    this.trafs = trafs;
                }
            }
            Iso.MovieFragmentBox = MovieFragmentBox;
            class MediaDataBox extends Box {
                constructor(chunks) {
                    super('mdat');
                    this.chunks = chunks;
                }
                layout(offset) {
                    var size = super.layout(offset);
                    this.chunks.forEach((chunk) => { size += chunk.length; });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    this.chunks.forEach((chunk) => {
                        data.set(chunk, this.offset + offset);
                        offset += chunk.length;
                    }, this);
                    return offset;
                }
            }
            Iso.MediaDataBox = MediaDataBox;
            class SampleEntry extends Box {
                constructor(format, dataReferenceIndex) {
                    super(format);
                    this.dataReferenceIndex = dataReferenceIndex;
                }
                layout(offset) {
                    this.size = super.layout(offset) + 8;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, this.dataReferenceIndex);
                    return offset + 8;
                }
            }
            Iso.SampleEntry = SampleEntry;
            class AudioSampleEntry extends SampleEntry {
                constructor(codingName, dataReferenceIndex, channelCount = 2, sampleSize = 16, sampleRate = 44100, otherBoxes = null) {
                    super(codingName, dataReferenceIndex);
                    this.channelCount = channelCount;
                    this.sampleSize = sampleSize;
                    this.sampleRate = sampleRate;
                    this.otherBoxes = otherBoxes;
                }
                layout(offset) {
                    var size = super.layout(offset) + 20;
                    this.otherBoxes && this.otherBoxes.forEach((box) => {
                        size += box.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, 0);
                    writeInt32(data, this.offset + offset + 8, (this.channelCount << 16) | this.sampleSize);
                    writeInt32(data, this.offset + offset + 12, 0);
                    writeInt32(data, this.offset + offset + 16, (this.sampleRate << 16));
                    offset += 20;
                    this.otherBoxes && this.otherBoxes.forEach((box) => {
                        offset += box.write(data);
                    });
                    return offset;
                }
            }
            Iso.AudioSampleEntry = AudioSampleEntry;
            Iso.COLOR_NO_ALPHA_VIDEO_SAMPLE_DEPTH = 0x0018;
            class VideoSampleEntry extends SampleEntry {
                constructor(codingName, dataReferenceIndex, width, height, compressorName = '', horizResolution = 72, vertResolution = 72, frameCount = 1, depth = Iso.COLOR_NO_ALPHA_VIDEO_SAMPLE_DEPTH, otherBoxes = null) {
                    super(codingName, dataReferenceIndex);
                    this.width = width;
                    this.height = height;
                    this.compressorName = compressorName;
                    this.horizResolution = horizResolution;
                    this.vertResolution = vertResolution;
                    this.frameCount = frameCount;
                    this.depth = depth;
                    this.otherBoxes = otherBoxes;
                    if (compressorName.length > 31) {
                        throw new Error('invalid compressor name');
                    }
                }
                layout(offset) {
                    var size = super.layout(offset) + 16 + 12 + 4 + 2 + 32 + 2 + 2;
                    this.otherBoxes && this.otherBoxes.forEach((box) => {
                        size += box.layout(offset + size);
                    });
                    return (this.size = size);
                }
                write(data) {
                    var offset = super.write(data);
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, 0);
                    writeInt32(data, this.offset + offset + 8, 0);
                    writeInt32(data, this.offset + offset + 12, 0);
                    offset += 16;
                    writeInt32(data, this.offset + offset, (this.width << 16) | this.height);
                    writeInt32(data, this.offset + offset + 4, encodeFloat_16_16(this.horizResolution));
                    writeInt32(data, this.offset + offset + 8, encodeFloat_16_16(this.vertResolution));
                    offset += 12;
                    writeInt32(data, this.offset + offset, 0);
                    writeInt32(data, this.offset + offset + 4, (this.frameCount << 16));
                    offset += 6;
                    data[this.offset + offset] = this.compressorName.length;
                    for (var i = 0; i < 31; i++) {
                        data[this.offset + offset + i + 1] = i < this.compressorName.length ? (this.compressorName.charCodeAt(i) & 127) : 0;
                    }
                    offset += 32;
                    writeInt32(data, this.offset + offset, (this.depth << 16) | 0xFFFF);
                    offset += 4;
                    this.otherBoxes && this.otherBoxes.forEach((box) => {
                        offset += box.write(data);
                    });
                    return offset;
                }
            }
            Iso.VideoSampleEntry = VideoSampleEntry;
            class RawTag extends Box {
                constructor(type, data) {
                    super(type);
                    this.data = data;
                }
                layout(offset) {
                    this.size = super.layout(offset) + this.data.length;
                    return this.size;
                }
                write(data) {
                    var offset = super.write(data);
                    data.set(this.data, this.offset + offset);
                    return offset + this.data.length;
                }
            }
            Iso.RawTag = RawTag;
        })(Iso = MP4.Iso || (MP4.Iso = {}));
    })(MP4 = RtmpJs.MP4 || (RtmpJs.MP4 = {}));
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var MP4;
    (function(MP4) {
        function hex(s) {
            var len = s.length >> 1;
            var arr = new Uint8Array(len);
            for (var i = 0; i < len; i++) {
                arr[i] = parseInt(s.substr(i * 2, 2), 16);
            }
            return arr;
        }
        var SOUNDRATES = [5500, 11025, 22050, 44100];
        var SOUNDFORMATS = ['PCM', 'ADPCM', 'MP3', 'PCM le', 'Nellymouser16', 'Nellymouser8', 'Nellymouser', 'G.711 A-law', 'G.711 mu-law', null, 'AAC', 'Speex', 'MP3 8khz'];
        var MP3_SOUND_CODEC_ID = 2;
        var AAC_SOUND_CODEC_ID = 10;
        let AudioPacketType;
        (function(AudioPacketType) {
            AudioPacketType[AudioPacketType["HEADER"] = 0] = "HEADER";
            AudioPacketType[AudioPacketType["RAW"] = 1] = "RAW";
        })(AudioPacketType || (AudioPacketType = {}));

        function parseAudiodata(data) {
            var i = 0;
            var packetType = AudioPacketType.RAW;
            var flags = data[i];
            var codecId = flags >> 4;
            var soundRateId = (flags >> 2) & 3;
            var sampleSize = flags & 2 ? 16 : 8;
            var channels = flags & 1 ? 2 : 1;
            var samples;
            i++;
            switch (codecId) {
                case AAC_SOUND_CODEC_ID:
                    var type = data[i++];
                    packetType = type;
                    samples = 1024;
                    break;
                case MP3_SOUND_CODEC_ID:
                    var version = (data[i + 1] >> 3) & 3;
                    var layer = (data[i + 1] >> 1) & 3;
                    samples = layer === 1 ? (version === 3 ? 1152 : 576) :
                        (layer === 3 ? 384 : 1152);
                    break;
            }
            return {
                codecDescription: SOUNDFORMATS[codecId],
                codecId: codecId,
                data: data.subarray(i),
                rate: SOUNDRATES[soundRateId],
                size: sampleSize,
                channels: channels,
                samples: samples,
                packetType: packetType
            };
        }
        var VIDEOCODECS = [null, 'JPEG', 'Sorenson', 'Screen', 'VP6', 'VP6 alpha', 'Screen2', 'AVC'];
        var VP6_VIDEO_CODEC_ID = 4;
        var AVC_VIDEO_CODEC_ID = 7;
        let VideoFrameType;
        (function(VideoFrameType) {
            VideoFrameType[VideoFrameType["KEY"] = 1] = "KEY";
            VideoFrameType[VideoFrameType["INNER"] = 2] = "INNER";
            VideoFrameType[VideoFrameType["DISPOSABLE"] = 3] = "DISPOSABLE";
            VideoFrameType[VideoFrameType["GENERATED"] = 4] = "GENERATED";
            VideoFrameType[VideoFrameType["INFO"] = 5] = "INFO";
        })(VideoFrameType || (VideoFrameType = {}));
        let VideoPacketType;
        (function(VideoPacketType) {
            VideoPacketType[VideoPacketType["HEADER"] = 0] = "HEADER";
            VideoPacketType[VideoPacketType["NALU"] = 1] = "NALU";
            VideoPacketType[VideoPacketType["END"] = 2] = "END";
        })(VideoPacketType || (VideoPacketType = {}));

        function parseVideodata(data) {
            var i = 0;
            var frameType = data[i] >> 4;
            var codecId = data[i] & 15;
            i++;
            var result = {
                frameType: frameType,
                codecId: codecId,
                codecDescription: VIDEOCODECS[codecId]
            };
            switch (codecId) {
                case AVC_VIDEO_CODEC_ID:
                    var type = data[i++];
                    result.packetType = type;
                    result.compositionTime = ((data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8)) >> 8;
                    i += 3;
                    break;
                case VP6_VIDEO_CODEC_ID:
                    result.packetType = VideoPacketType.NALU;
                    result.horizontalOffset = (data[i] >> 4) & 15;
                    result.verticalOffset = data[i] & 15;
                    result.compositionTime = 0;
                    i++;
                    break;
            }
            result.data = data.subarray(i);
            return result;
        }
        var AUDIO_PACKET = 8;
        var VIDEO_PACKET = 9;
          //@NOTE: Pravin, we can change this: packets = sec*{fps + 8} 
        var MAX_PACKETS_IN_CHUNK = 69;
        var SPLIT_AT_KEYFRAMES = false;
        let MP4MuxState;
        (function(MP4MuxState) {
            MP4MuxState[MP4MuxState["CAN_GENERATE_HEADER"] = 0] = "CAN_GENERATE_HEADER";
            MP4MuxState[MP4MuxState["NEED_HEADER_DATA"] = 1] = "NEED_HEADER_DATA";
            MP4MuxState[MP4MuxState["MAIN_PACKETS"] = 2] = "MAIN_PACKETS";
        })(MP4MuxState || (MP4MuxState = {}));
        class MP4Mux {
            constructor(metadata) {
                this.oncodecinfo = function(codecs) {};
                this.ondata = function(data) {
                    throw new Error('MP4Mux.ondata is not set');
                };
                this.metadata = metadata;
                this.trackStates = this.metadata.tracks.map((t, index) => {
                    var state = {
                        trackId: index + 1,
                        trackInfo: t,
                        cachedDuration: 0,
                        samplesProcessed: 0,
                        initializationData: []
                    };
                    if (this.metadata.audioTrackId === index) {
                        this.audioTrackState = state;
                    }
                    if (this.metadata.videoTrackId === index) {
                        this.videoTrackState = state;
                    }
                    return state;
                }, this);
                this._checkIfNeedHeaderData();
                this.filePos = 0;
                this.cachedPackets = [];
                this.chunkIndex = 0;
            }
           pushPacket(fps, buffSec, type, data, timestamp) {
                if (this.state === MP4MuxState.CAN_GENERATE_HEADER) {
                    this._tryGenerateHeader();
                }
                switch (type) {
                    case AUDIO_PACKET:
                        var audioTrack = this.audioTrackState;
                        var audioPacket = parseAudiodata(data);
                        if (!audioTrack || audioTrack.trackInfo.codecId !== audioPacket.codecId) {
                            throw new Error('Unexpected audio packet codec: ' + audioPacket.codecDescription);
                        }
                        switch (audioPacket.codecId) {
                            default: throw new Error('Unsupported audio codec: ' + audioPacket.codecDescription);
                            case MP3_SOUND_CODEC_ID:
                                    break;
                            case AAC_SOUND_CODEC_ID:
                                    if (audioPacket.packetType === AudioPacketType.HEADER) {
                                        audioTrack.initializationData.push(audioPacket.data);
                                        return;
                                    }
                                break;
                        }
                        this.cachedPackets.push({ packet: audioPacket, timestamp: timestamp, trackId: audioTrack.trackId });
                        break;
                    case VIDEO_PACKET:
                        var videoTrack = this.videoTrackState;
                        //console.log("TrackState: " + videoTrack.trackInfo.framerate);
                        videoTrack.trackInfo.framerate = fps;
                        var videoPacket = parseVideodata(data);
                        if (!videoTrack || videoTrack.trackInfo.codecId !== videoPacket.codecId) {
                            throw new Error('Unexpected video packet codec: ' + videoPacket.codecDescription);
                        }
                        switch (videoPacket.codecId) {
                            default: throw new Error('unsupported video codec: ' + videoPacket.codecDescription);
                            case VP6_VIDEO_CODEC_ID:
                                    break;
                            case AVC_VIDEO_CODEC_ID:
                                    if (videoPacket.packetType === VideoPacketType.HEADER) {
                                        videoTrack.initializationData.push(videoPacket.data);
                                        return;
                                    }
                                break;
                        }
                        this.cachedPackets.push({ packet: videoPacket, timestamp: timestamp, trackId: videoTrack.trackId });
                        break;
                    default:
                        throw new Error('unknown packet type: ' + type);
                }
                if (this.state === MP4MuxState.NEED_HEADER_DATA) {
                    this._tryGenerateHeader();
                }
			    if (0 == buffSec) {
                    MAX_PACKETS_IN_CHUNK = 1
                } else {
                    MAX_PACKETS_IN_CHUNK = (buffSec*(fps + 8))
                }
                if (this.cachedPackets.length >= MAX_PACKETS_IN_CHUNK &&
                    this.state === MP4MuxState.MAIN_PACKETS) {
                    this._chunk();
                }
            }
            flush() {
                if (this.cachedPackets.length > 0) {
                    this._chunk();
                }
            }
            _checkIfNeedHeaderData() {
                if (this.trackStates.some((ts) => ts.trackInfo.codecId === AAC_SOUND_CODEC_ID || ts.trackInfo.codecId === AVC_VIDEO_CODEC_ID)) {
                    this.state = MP4MuxState.NEED_HEADER_DATA;
                } else {
                    this.state = MP4MuxState.CAN_GENERATE_HEADER;
                }
            }
            _tryGenerateHeader() {
                var allInitializationDataExists = this.trackStates.every((ts) => {
                    switch (ts.trackInfo.codecId) {
                        case AAC_SOUND_CODEC_ID:
                        case AVC_VIDEO_CODEC_ID:
                            return ts.initializationData.length > 0;
                        default:
                            return true;
                    }
                });
                if (!allInitializationDataExists) {
                    return;
                }
                var brands = ['isom'];
                var audioDataReferenceIndex = 1,
                    videoDataReferenceIndex = 1;
                var traks = [];
                for (var i = 0; i < this.trackStates.length; i++) {
                    var trackState = this.trackStates[i];
                    var trackInfo = trackState.trackInfo;
                    var sampleEntry;
                    switch (trackInfo.codecId) {
                        case AAC_SOUND_CODEC_ID:
                            var audioSpecificConfig = trackState.initializationData[0];
                            sampleEntry = new MP4.Iso.AudioSampleEntry('mp4a', audioDataReferenceIndex, trackInfo.channels, trackInfo.samplesize, trackInfo.samplerate);
                            var esdsData = new Uint8Array(41 + audioSpecificConfig.length);
                            esdsData.set(hex('0000000003808080'), 0);
                            esdsData[8] = 32 + audioSpecificConfig.length;
                            esdsData.set(hex('00020004808080'), 9);
                            esdsData[16] = 18 + audioSpecificConfig.length;
                            esdsData.set(hex('40150000000000FA000000000005808080'), 17);
                            esdsData[34] = audioSpecificConfig.length;
                            esdsData.set(audioSpecificConfig, 35);
                            esdsData.set(hex('068080800102'), 35 + audioSpecificConfig.length);
                            sampleEntry.otherBoxes = [
                                new MP4.Iso.RawTag('esds', esdsData)
                            ];
                            var objectType = (audioSpecificConfig[0] >> 3);
                            trackState.mimeTypeCodec = 'mp4a.40.' + objectType;
                            break;
                        case MP3_SOUND_CODEC_ID:
                            sampleEntry = new MP4.Iso.AudioSampleEntry('.mp3', audioDataReferenceIndex, trackInfo.channels, trackInfo.samplesize, trackInfo.samplerate);
                            trackState.mimeTypeCodec = 'mp3';
                            break;
                        case AVC_VIDEO_CODEC_ID:
                            var avcC = trackState.initializationData[0];
                            sampleEntry = new MP4.Iso.VideoSampleEntry('avc1', videoDataReferenceIndex, trackInfo.width, trackInfo.height);
                            sampleEntry.otherBoxes = [
                                new MP4.Iso.RawTag('avcC', avcC)
                            ];
                            var codecProfile = (avcC[1] << 16) | (avcC[2] << 8) | avcC[3];
                            trackState.mimeTypeCodec = 'avc1.' + (0x1000000 | codecProfile).toString(16).substr(1);
                            brands.push('iso2', 'avc1', 'mp41');
                            break;
                        case VP6_VIDEO_CODEC_ID:
                            sampleEntry = new MP4.Iso.VideoSampleEntry('VP6F', videoDataReferenceIndex, trackInfo.width, trackInfo.height);
                            sampleEntry.otherBoxes = [
                                new MP4.Iso.RawTag('glbl', hex('00'))
                            ];
                            trackState.mimeTypeCodec = 'avc1.42001E';
                            break;
                        default:
                            throw new Error('not supported track type');
                    }
                    var trak;
                    var trakFlags = MP4.Iso.TrackHeaderFlags.TRACK_ENABLED | MP4.Iso.TrackHeaderFlags.TRACK_IN_MOVIE;
                    if (trackState === this.audioTrackState) {
                        trak = new MP4.Iso.TrackBox(new MP4.Iso.TrackHeaderBox(trakFlags, trackState.trackId, -1, 0, 0, 1.0, i), new MP4.Iso.MediaBox(new MP4.Iso.MediaHeaderBox(trackInfo.timescale, -1, trackInfo.language), new MP4.Iso.HandlerBox('soun', 'SoundHandler'), new MP4.Iso.MediaInformationBox(new MP4.Iso.SoundMediaHeaderBox(), new MP4.Iso.DataInformationBox(new MP4.Iso.DataReferenceBox([new MP4.Iso.DataEntryUrlBox(MP4.Iso.SELF_CONTAINED_DATA_REFERENCE_FLAG)])), new MP4.Iso.SampleTableBox(new MP4.Iso.SampleDescriptionBox([sampleEntry]), new MP4.Iso.RawTag('stts', hex('0000000000000000')), new MP4.Iso.RawTag('stsc', hex('0000000000000000')), new MP4.Iso.RawTag('stsz', hex('000000000000000000000000')), new MP4.Iso.RawTag('stco', hex('0000000000000000'))))));
                    } else if (trackState === this.videoTrackState) {
                        trak = new MP4.Iso.TrackBox(new MP4.Iso.TrackHeaderBox(trakFlags, trackState.trackId, -1, trackInfo.width, trackInfo.height, 0, i), new MP4.Iso.MediaBox(new MP4.Iso.MediaHeaderBox(trackInfo.timescale, -1, trackInfo.language), new MP4.Iso.HandlerBox('vide', 'VideoHandler'), new MP4.Iso.MediaInformationBox(new MP4.Iso.VideoMediaHeaderBox(), new MP4.Iso.DataInformationBox(new MP4.Iso.DataReferenceBox([new MP4.Iso.DataEntryUrlBox(MP4.Iso.SELF_CONTAINED_DATA_REFERENCE_FLAG)])), new MP4.Iso.SampleTableBox(new MP4.Iso.SampleDescriptionBox([sampleEntry]), new MP4.Iso.RawTag('stts', hex('0000000000000000')), new MP4.Iso.RawTag('stsc', hex('0000000000000000')), new MP4.Iso.RawTag('stsz', hex('000000000000000000000000')), new MP4.Iso.RawTag('stco', hex('0000000000000000'))))));
                    }
                    traks.push(trak);
                }
                var mvex = new MP4.Iso.MovieExtendsBox(null, [
                    new MP4.Iso.TrackExtendsBox(1, 1, 0, 0, 0),
                    new MP4.Iso.TrackExtendsBox(2, 1, 0, 0, 0)
                ], null);
                var udat = new MP4.Iso.BoxContainerBox('udat', [
                    new MP4.Iso.MetaBox(new MP4.Iso.RawTag('hdlr', hex('00000000000000006D6469726170706C000000000000000000')), [new MP4.Iso.RawTag('ilst', hex('00000025A9746F6F0000001D6461746100000001000000004C61766635342E36332E313034'))])
                ]);
                var mvhd = new MP4.Iso.MovieHeaderBox(1000, 0, this.trackStates.length + 1);
                var moov = new MP4.Iso.MovieBox(mvhd, traks, mvex, udat);
                var ftype = new MP4.Iso.FileTypeBox('isom', 0x00000200, brands);
                var ftypeSize = ftype.layout(0);
                var moovSize = moov.layout(ftypeSize);
                var header = new Uint8Array(ftypeSize + moovSize);
                ftype.write(header);
                moov.write(header);
                this.oncodecinfo(this.trackStates.map((ts) => ts.mimeTypeCodec));
                this.ondata(header);
                this.filePos += header.length;
                this.state = MP4MuxState.MAIN_PACKETS;
            }
            _chunk() {
                var cachedPackets = this.cachedPackets;
                if (SPLIT_AT_KEYFRAMES && this.videoTrackState) {
                    var j = cachedPackets.length - 1;
                    var videoTrackId = this.videoTrackState.trackId;
                    while (j > 0 &&
                        (cachedPackets[j].trackId !== videoTrackId || cachedPackets[j].packet.frameType !== VideoFrameType.KEY)) {
                        j--;
                    }
                    if (j > 0) {
                        cachedPackets = cachedPackets.slice(0, j);
                    }
                }
                if (cachedPackets.length === 0) {
                    return;
                }
                var tdatParts = [];
                var tdatPosition = 0;
                var trafs = [];
                var trafDataStarts = [];
                for (var i = 0; i < this.trackStates.length; i++) {
                    var trackState = this.trackStates[i];
                    var trackInfo = trackState.trackInfo;
                    var trackId = trackState.trackId;
                    var trackPackets = cachedPackets.filter((cp) => cp.trackId === trackId);
                    if (trackPackets.length === 0) {
                        continue;
                    }
                    var tfdt = new MP4.Iso.TrackFragmentBaseMediaDecodeTimeBox(trackState.cachedDuration);
                    var tfhd;
                    var trun;
                    var trunSamples;
                    trafDataStarts.push(tdatPosition);
                    switch (trackInfo.codecId) {
                        case AAC_SOUND_CODEC_ID:
                        case MP3_SOUND_CODEC_ID:
                            trunSamples = [];
                            for (var j = 0; j < trackPackets.length; j++) {
                                var audioPacket = trackPackets[j].packet;
                                var audioFrameDuration = Math.round(audioPacket.samples * trackInfo.timescale / trackInfo.samplerate);
                                tdatParts.push(audioPacket.data);
                                tdatPosition += audioPacket.data.length;
                                trunSamples.push({ duration: audioFrameDuration, size: audioPacket.data.length });
                                trackState.samplesProcessed += audioPacket.samples;
                            }
                            var tfhdFlags = MP4.Iso.TrackFragmentFlags.DEFAULT_SAMPLE_FLAGS_PRESENT;
                            tfhd = new MP4.Iso.TrackFragmentHeaderBox(tfhdFlags, trackId, 0, 0, 0, 0, MP4.Iso.SampleFlags.SAMPLE_DEPENDS_ON_NO_OTHERS);
                            var trunFlags = MP4.Iso.TrackRunFlags.DATA_OFFSET_PRESENT |
                                MP4.Iso.TrackRunFlags.SAMPLE_DURATION_PRESENT | MP4.Iso.TrackRunFlags.SAMPLE_SIZE_PRESENT;
                            trun = new MP4.Iso.TrackRunBox(trunFlags, trunSamples, 0, 0);
                            trackState.cachedDuration = Math.round(trackState.samplesProcessed * trackInfo.timescale / trackInfo.samplerate);
                            break;
                        case AVC_VIDEO_CODEC_ID:
                        case VP6_VIDEO_CODEC_ID:
                            trunSamples = [];
                            var samplesProcessed = trackState.samplesProcessed;
                            var decodeTime = samplesProcessed * trackInfo.timescale / trackInfo.framerate;
                            var lastTime = Math.round(decodeTime);
                            for (var j = 0; j < trackPackets.length; j++) {
                                var videoPacket = trackPackets[j].packet;
                                samplesProcessed++;
                                var nextTime = Math.round(samplesProcessed * trackInfo.timescale / trackInfo.framerate);
                                var videoFrameDuration = nextTime - lastTime;
                                lastTime = nextTime;
                                var compositionTime = Math.round(samplesProcessed * trackInfo.timescale / trackInfo.framerate +
                                    videoPacket.compositionTime * trackInfo.timescale / 1000);
                                tdatParts.push(videoPacket.data);
                                tdatPosition += videoPacket.data.length;
                                var frameFlags = videoPacket.frameType === VideoFrameType.KEY ?
                                    MP4.Iso.SampleFlags.SAMPLE_DEPENDS_ON_NO_OTHERS :
                                    (MP4.Iso.SampleFlags.SAMPLE_DEPENDS_ON_OTHER | MP4.Iso.SampleFlags.SAMPLE_IS_NOT_SYNC);
                                trunSamples.push({
                                    duration: videoFrameDuration,
                                    size: videoPacket.data.length,
                                    flags: frameFlags,
                                    compositionTimeOffset: (compositionTime - nextTime)
                                });
                            }
                            var tfhdFlags = MP4.Iso.TrackFragmentFlags.DEFAULT_SAMPLE_FLAGS_PRESENT;
                            tfhd = new MP4.Iso.TrackFragmentHeaderBox(tfhdFlags, trackId, 0, 0, 0, 0, MP4.Iso.SampleFlags.SAMPLE_DEPENDS_ON_NO_OTHERS);
                            var trunFlags = MP4.Iso.TrackRunFlags.DATA_OFFSET_PRESENT |
                                MP4.Iso.TrackRunFlags.SAMPLE_DURATION_PRESENT | MP4.Iso.TrackRunFlags.SAMPLE_SIZE_PRESENT |
                                MP4.Iso.TrackRunFlags.SAMPLE_FLAGS_PRESENT | MP4.Iso.TrackRunFlags.SAMPLE_COMPOSITION_TIME_OFFSET;
                            trun = new MP4.Iso.TrackRunBox(trunFlags, trunSamples, 0, 0);
                            trackState.cachedDuration = lastTime;
                            trackState.samplesProcessed = samplesProcessed;
                            break;
                        default:
                            throw new Error('Un codec');
                    }
                    var traf = new MP4.Iso.TrackFragmentBox(tfhd, tfdt, trun);
                    trafs.push(traf);
                }
                this.cachedPackets.splice(0, cachedPackets.length);
                var moofHeader = new MP4.Iso.MovieFragmentHeaderBox(++this.chunkIndex);
                var moof = new MP4.Iso.MovieFragmentBox(moofHeader, trafs);
                var moofSize = moof.layout(0);
                var mdat = new MP4.Iso.MediaDataBox(tdatParts);
                var mdatSize = mdat.layout(moofSize);
                var tdatOffset = moofSize + 8;
                for (var i = 0; i < trafs.length; i++) {
                    trafs[i].run.dataOffset = tdatOffset + trafDataStarts[i];
                }
                var chunk = new Uint8Array(moofSize + mdatSize);
                moof.write(chunk);
                mdat.write(chunk);
                this.ondata(chunk);
                this.filePos += chunk.length;
            }
        }
        MP4.MP4Mux = MP4Mux;

        function parseFLVMetadata(metadata) {
            var tracks = [];
            var audioTrackId = -1;
            var videoTrackId = -1;
            var duration = +metadata.asGetPublicProperty('duration');
            var audioCodec, audioCodecId;
            var audioCodecCode = metadata.asGetPublicProperty('audiocodecid');
            switch (audioCodecCode) {
                case MP3_SOUND_CODEC_ID:
                case 'mp3':
                    audioCodec = 'mp3';
                    audioCodecId = MP3_SOUND_CODEC_ID;
                    break;
                case AAC_SOUND_CODEC_ID:
                case 'mp4a':
                    audioCodec = 'mp4a';
                    audioCodecId = AAC_SOUND_CODEC_ID;
                    break;
                default:
  //@NOTE: Pravin, commented as this was not playing video-only feed.
                    //if (!isNaN(audioCodecCode)) {
                    //    throw new Error('Unsupported audio codec: ' + audioCodecCode);
                    //}
                    audioCodec = null;
                    audioCodecId = -1;
                    break;
            }
            var videoCodec, videoCodecId;
            var videoCodecCode = metadata.asGetPublicProperty('videocodecid');
            switch (videoCodecCode) {
                case VP6_VIDEO_CODEC_ID:
                case 'vp6f':
                    videoCodec = 'vp6f';
                    videoCodecId = VP6_VIDEO_CODEC_ID;
                    break;
                case AVC_VIDEO_CODEC_ID:
                case 'avc1':
                    videoCodec = 'avc1';
                    videoCodecId = AVC_VIDEO_CODEC_ID;
                    break;
                default:
				  //@NOTE: Pravin, sometimes we get videoCodecCode=0 because of wrong metadata, so patch here.
                    console.log("@Error: Found invalid video codec: " + videoCodecCode + ", defaulting to AVC1/H.264");
                    videoCodec = 'avc1';
                    videoCodecId = AVC_VIDEO_CODEC_ID;
                    /*
                    if (!isNaN(videoCodecCode)) {
                        throw new Error('Unsupported video codec: ' + videoCodecCode);
                    }
                    videoCodec = null;
                    videoCodecId = -1;
					 */
                    break;
            }
            var audioTrack = (audioCodec === null) ? null : {
                codecDescription: audioCodec,
                codecId: audioCodecId,
                language: 'und',
                timescale: +metadata.asGetPublicProperty('audiosamplerate') || 44100,
                samplerate: +metadata.asGetPublicProperty('audiosamplerate') || 44100,
                channels: +metadata.asGetPublicProperty('audiochannels') || 2,
                samplesize: 16
            };
            var videoTrack = (videoCodec === null) ? null : {
                codecDescription: videoCodec,
                codecId: videoCodecId,
                language: 'und',
                timescale: 60000,
                framerate: +metadata.asGetPublicProperty('videoframerate') ||
                    +metadata.asGetPublicProperty('framerate'),
                width: +metadata.asGetPublicProperty('width'),
                height: +metadata.asGetPublicProperty('height')
            };
            var trackInfos = metadata.asGetPublicProperty('trackinfo');
            if (trackInfos) {
                for (var i = 0; i < trackInfos.length; i++) {
                    var info = trackInfos[i];
                    var sampleDescription = info.asGetPublicProperty('sampledescription')[0];
                    if (sampleDescription.asGetPublicProperty('sampletype') === audioCodecCode) {
                        audioTrack.language = info.asGetPublicProperty('language');
                        audioTrack.timescale = +info.asGetPublicProperty('timescale');
                    } else if (sampleDescription.asGetPublicProperty('sampletype') === videoCodecCode) {
                        videoTrack.language = info.asGetPublicProperty('language');
                        videoTrack.timescale = +info.asGetPublicProperty('timescale');
                    }
                }
            }
            if (videoTrack) {
                videoTrackId = tracks.length;
                tracks.push(videoTrack);
            }
            if (audioTrack) {
                audioTrackId = tracks.length;
                tracks.push(audioTrack);
            }
            return {
                tracks: tracks,
                duration: duration,
                audioTrackId: audioTrackId,
                videoTrackId: videoTrackId
            };
        }
        MP4.parseFLVMetadata = parseFLVMetadata;

        function splitMetadata(metadata) {
            var tracks = [];
            if (metadata.audioTrackId >= 0) {
                tracks.push({
                    tracks: [metadata.tracks[metadata.audioTrackId]],
                    duration: metadata.duration,
                    audioTrackId: 0,
                    videoTrackId: -1
                });
            }
            if (metadata.videoTrackId >= 0) {
                tracks.push({
                    tracks: [metadata.tracks[metadata.videoTrackId]],
                    duration: metadata.duration,
                    audioTrackId: -1,
                    videoTrackId: 0
                });
            }
            return tracks;
        }
        MP4.splitMetadata = splitMetadata;
    })(MP4 = RtmpJs.MP4 || (RtmpJs.MP4 = {}));
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var FLV;
    (function(FLV) {
        class FLVParser {
            constructor() {
                this.state = 0;
                this.state = 0;
                this.buffer = new ArrayBuffer(1024);
                this.bufferSize = 0;
                this.previousTagSize = 0;
                this.onError = null;
                this.onHeader = null;
                this.onTag = null;
                this.onClose = null;
            }
            push(data) {
                var parseBuffer;
                if (this.bufferSize > 0) {
                    var needLength = this.bufferSize + data.length;
                    if (this.buffer.byteLength < needLength) {
                        var tmp = new Uint8Array(this.buffer, 0, this.bufferSize);
                        this.buffer = new ArrayBuffer(needLength);
                        parseBuffer = new Uint8Array(this.buffer);
                        parseBuffer.set(tmp);
                    } else {
                        parseBuffer = new Uint8Array(this.buffer, 0, needLength);
                    }
                    parseBuffer.set(data, this.bufferSize);
                } else {
                    parseBuffer = data;
                }
                var parsed = 0,
                    end = parseBuffer.length;
                while (parsed < end) {
                    var chunkParsed = 0;
                    switch (this.state) {
                        case 0:
                            if (parsed + 9 > end) {
                                break;
                            }
                            var headerLength = (parseBuffer[parsed + 5] << 24) | (parseBuffer[parsed + 6] << 16) |
                                (parseBuffer[parsed + 7] << 8) | parseBuffer[parsed + 8];
                            if (headerLength < 9) {
                                this._error('Invalid header length');
                                break;
                            }
                            if (parsed + headerLength > end) {
                                break;
                            }
                            if (parseBuffer[parsed] !== 0x46 ||
                                parseBuffer[parsed + 1] !== 0x4C ||
                                parseBuffer[parsed + 2] !== 0x56 ||
                                parseBuffer[parsed + 3] !== 1 ||
                                (parseBuffer[parsed + 4] & 0xFA) !== 0) {
                                this._error('Invalid FLV header');
                                break;
                            }
                            var flags = parseBuffer[parsed + 4];
                            var extra = headerLength > 9 ? parseBuffer.subarray(parsed + 9, parsed + headerLength) : null;
                            this.onHeader && this.onHeader({
                                hasAudio: !!(flags & 4),
                                hasVideo: !!(flags & 1),
                                extra: extra
                            });
                            this.state = 2;
                            chunkParsed = headerLength;
                            break;
                        case 2:
                            if (parsed + 4 + 11 > end) {
                                break;
                            }
                            var previousTagSize = (parseBuffer[parsed + 0] << 24) | (parseBuffer[parsed + 1] << 16) |
                                (parseBuffer[parsed + 2] << 8) | parseBuffer[parsed + 3];
                            if (previousTagSize !== this.previousTagSize) {
                                this._error('Invalid PreviousTagSize');
                                break;
                            }
                            var dataSize = (parseBuffer[parsed + 5] << 16) |
                                (parseBuffer[parsed + 6] << 8) | parseBuffer[parsed + 7];
                            var dataOffset = parsed + 4 + 11;
                            if (dataOffset + dataSize > end) {
                                break;
                            }
                            var flags = parseBuffer[parsed + 4];
                            var streamID = (parseBuffer[parsed + 12] << 16) |
                                (parseBuffer[parsed + 13] << 8) | parseBuffer[parsed + 14];
                            if (streamID !== 0 || (flags & 0xC0) !== 0) {
                                this._error('Invalid FLV tag');
                                break;
                            }
                            var dataType = flags & 0x1F;
                            if (dataType !== 8 && dataType !== 9 && dataType !== 18) {
                                this._error('Invalid FLV tag type');
                                break;
                            }
                            var needPreprocessing = !!(flags & 0x20);
                            var timestamp = (parseBuffer[parsed + 8] << 16) |
                                (parseBuffer[parsed + 9] << 8) | parseBuffer[parsed + 10] |
                                (parseBuffer[parseBuffer + 11] << 24);
                            this.onTag && this.onTag({
                                type: dataType,
                                needPreprocessing: needPreprocessing,
                                timestamp: timestamp,
                                data: parseBuffer.subarray(dataOffset, dataOffset + dataSize)
                            });
                            chunkParsed += 4 + 11 + dataSize;
                            this.previousTagSize = dataSize + 11;
                            this.state = 2;
                            break;
                        default:
                            throw new Error('invalid state');
                    }
                    if (chunkParsed === 0) {
                        break;
                    }
                    parsed += chunkParsed;
                }
                if (parsed < parseBuffer.length) {
                    this.bufferSize = parseBuffer.length - parsed;
                    if (this.buffer.byteLength < this.bufferSize) {
                        this.buffer = new ArrayBuffer(this.bufferSize);
                    }
                    new Uint8Array(this.buffer).set(parseBuffer.subarray(parsed));
                } else {
                    this.bufferSize = 0;
                }
            }
            _error(message) {
                this.state = -1;
                this.onError && this.onError(message);
            }
            close() {
                this.onClose && this.onClose();
            }
        }
        FLV.FLVParser = FLVParser;
    })(FLV = RtmpJs.FLV || (RtmpJs.FLV = {}));
})(RtmpJs || (RtmpJs = {}));
var RtmpJs;
(function(RtmpJs) {
    var MP3;
    (function(MP3) {
        var BitratesMap = [
            32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448,
            32, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384,
            32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320,
            32, 48, 56, 64, 80, 96, 112, 128, 144, 160, 176, 192, 224, 256,
            8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160
        ];
        var SamplingRateMap = [44100, 48000, 32000, 22050, 24000, 16000, 11025, 12000, 8000];
        class MP3Parser {
            constructor() {
                this.buffer = null;
                this.bufferSize = 0;
            }
            push(data) {
                var length;
                if (this.bufferSize > 0) {
                    var needBuffer = data.length + this.bufferSize;
                    if (!this.buffer || this.buffer.length < needBuffer) {
                        var newBuffer = new Uint8Array(needBuffer);
                        if (this.bufferSize > 0) {
                            newBuffer.set(this.buffer.subarray(0, this.bufferSize));
                        }
                        this.buffer = newBuffer;
                    }
                    this.buffer.set(data, this.bufferSize);
                    this.bufferSize = needBuffer;
                    data = this.buffer;
                    length = needBuffer;
                } else {
                    length = data.length;
                }
                var offset = 0;
                var parsed;
                while (offset < length &&
                    (parsed = this._parse(data, offset, length)) > 0) {
                    offset += parsed;
                }
                var tail = length - offset;
                if (tail > 0) {
                    if (!this.buffer || this.buffer.length < tail) {
                        this.buffer = new Uint8Array(data.subarray(offset, length));
                    } else {
                        this.buffer.set(data.subarray(offset, length));
                    }
                }
                this.bufferSize = tail;
            }
            _parse(data, start, end) {
                if (start + 2 > end) {
                    return -1;
                }
                if (data[start] === 0xFF || (data[start + 1] & 0xE0) === 0xE0) {
                    if (start + 24 > end) {
                        return -1;
                    }
                    var headerB = (data[start + 1] >> 3) & 3;
                    var headerC = (data[start + 1] >> 1) & 3;
                    var headerE = (data[start + 2] >> 4) & 15;
                    var headerF = (data[start + 2] >> 2) & 3;
                    var headerG = !!(data[start + 2] & 2);
                    if (headerB !== 1 && headerE !== 0 && headerE !== 15 && headerF !== 3) {
                        var columnInBitrates = headerB === 3 ? (3 - headerC) : (headerC === 3 ? 3 : 4);
                        var bitRate = BitratesMap[columnInBitrates * 14 + headerE - 1] * 1000;
                        var columnInSampleRates = headerB === 3 ? 0 : headerB === 2 ? 1 : 2;
                        var sampleRate = SamplingRateMap[columnInSampleRates * 3 + headerF];
                        var padding = headerG ? 1 : 0;
                        var frameLength = headerC === 3 ?
                            ((headerB === 3 ? 12 : 6) * bitRate / sampleRate + padding) << 2 :
                            ((headerB === 3 ? 144 : 72) * bitRate / sampleRate + padding) | 0;
                        if (start + frameLength > end) {
                            return -1;
                        }
                        if (this.onFrame) {
                            this.onFrame(data.subarray(start, start + frameLength));
                        }
                        return frameLength;
                    }
                }
                var offset = start + 2;
                while (offset < end) {
                    if (data[offset - 1] === 0xFF && (data[offset] & 0xE0) === 0xE0) {
                        if (this.onNoise) {
                            this.onNoise(data.subarray(start, offset - 1));
                        }
                        return offset - start - 1;
                    }
                    offset++;
                }
                return -1;
            }
            close() {
                if (this.bufferSize > 0) {
                    if (this.onNoise) {
                        this.onNoise(this.buffer.subarray(0, this.bufferSize));
                    }
                }
                this.buffer = null;
                this.bufferSize = 0;
                if (this.onClose) {
                    this.onClose();
                }
            }
        }
        MP3.MP3Parser = MP3Parser;
    })(MP3 = RtmpJs.MP3 || (RtmpJs.MP3 = {}));
})(RtmpJs || (RtmpJs = {}));
