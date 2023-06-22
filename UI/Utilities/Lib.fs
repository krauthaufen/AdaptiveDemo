namespace AdaptiveDemo

open System
open Aardvark.Base
open Aardvark.Dom
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.WebAssembly.Dom
open FSharp.Data.Adaptive

module UI =


    // generator code:
    // var text = "let cacheContent =\n    [|\n"
    // for(var i = 0; i < localStorage.length; i++) {
    //     var key = localStorage.key(i);
    //     let value = localStorage.getItem(key)
    //     if(key.startsWith("pointshare")) continue;
    //     text += "        \"" + key + "\", \"" + value + "\"\n";
    // }
    // text += "    |]";
    // console.log(text);
    let cacheContent =
        [|
            "pick_xo/Bb/IvRVP0g6hhEv4Ya7DYNgk=O4mOgfh4hVX/2WB2YBo7qCIZlhc=_Colors:0_PickId:1", "CkdMU0xTaGFkZXIDAAAAAAAAABdEaWZmdXNlQ29sb3JDb29yZGluYXRlcxdEaWZmdXNlQ29sb3JDb29yZGluYXRlcwQCAyABAAAAAAEAAAAHTm9ybWFscwdOb3JtYWxzBAMDIAEAAAAAAgAAAAlQb3NpdGlvbnMJUG9zaXRpb25zBAQDIAEAAAAAAgAAAAAAAAAJQ29sb3JzT3V0BkNvbG9ycwQEAyABAAAAAAEAAAAJUGlja0lkT3V0BlBpY2tJZAQEAgEgAQAAAAABAAAADmRpZmZ1c2VTYW1wbGVy/////wEAAAAOZGlmZnVzZVNhbXBsZXL/////CQEAAAAEBAMgAAAAAQAAABNEaWZmdXNlQ29sb3JUZXh0dXJlAQAAAAABAAAAAAABBAAAAAAAAAAAAAAAAAAAAAAAAwAAAAZHbG9iYWz/////Bkdsb2JhbP////8QAAAAAQAAAAZQaWNrSWQAAAAAAgEgCFBlck1vZGVs/////whQZXJNb2RlbP////+AAAAAAgAAAApNb2RlbFRyYWZvAAAAAAUEBAMgEU1vZGVsVmlld1RyYWZvSW52QAAAAAUEBAMgB1BlclZpZXf/////B1BlclZpZXf/////QAAAAAEAAAANVmlld1Byb2pUcmFmbwAAAAAFBAQDIAAAAAAAAgAAAAAAAAAAAAAABG1haW4DAAAAAAAAABdEaWZmdXNlQ29sb3JDb29yZGluYXRlcxdEaWZmdXNlQ29sb3JDb29yZGluYXRlcwQCAyABAAAAAAEAAAAHTm9ybWFscwdOb3JtYWxzBAMDIAEAAAAAAgAAAAlQb3NpdGlvbnMJUG9zaXRpb25zBAQDIAEAAAAAAgAAAAAAAAAaZnNfRGlmZnVzZUNvbG9yQ29vcmRpbmF0ZXMXRGlmZnVzZUNvbG9yQ29vcmRpbmF0ZXMEAgMgAQAAAAABAAAAEmZzX1ZpZXdTcGFjZU5vcm1hbA9WaWV3U3BhY2VOb3JtYWwEAwMgAQAAAAAAAAAAAAAAAAAAAAACAAAACFBlck1vZGVsB1BlclZpZXcAAAAAAQAAAAlub3JtYWxpemUEAwMgAQAAAAQDAyAAAAAAAQAAAAEAAAABAAAAC2dsX1Bvc2l0aW9uBAQDIAQAAAAEAAAABG1haW4CAAAAAAAAABpmc19EaWZmdXNlQ29sb3JDb29yZGluYXRlcxdEaWZmdXNlQ29sb3JDb29yZGluYXRlcwQCAyABAAAAAAEAAAASZnNfVmlld1NwYWNlTm9ybWFsD1ZpZXdTcGFjZU5vcm1hbAQDAyABAAAAAAIAAAAAAAAACUNvbG9yc091dAZDb2xvcnMEBAMgAQAAAAABAAAACVBpY2tJZE91dAZQaWNrSWQEBAIBIAEAAAAAAQAAAA5kaWZmdXNlU2FtcGxlcgAAAAAAAAAAAQAAAAZHbG9iYWwAAAAABgAAAAVmbG9vcgMgAQAAAAMgDmZsb2F0Qml0c1RvSW50AgEgAQAAAAMgCW5vcm1hbGl6ZQQDAyABAAAABAMDIANhYnMDIAEAAAADIAxnbF9GcmFnQ29vcmQEBAMgAAAAAAd0ZXh0dXJlBAQDIAIAAAAJAQAAAAQEAyAAAAAEAgMgAAAAAAIAAAAAAAAAAQAAAAtnbF9Qb3NpdGlvbgQEAyABAAAAAQAAAAxnbF9GcmFnRGVwdGgDIP4ZI3ZlcnNpb24gMzAwIGVzDQoNCg0KDQpsYXlvdXQoc3RkMTQwKQ0KdW5pZm9ybSBQZXJNb2RlbA0Kew0KICAgIG1hdDR4NCBNb2RlbFRyYWZvOw0KICAgIG1hdDR4NCBNb2RlbFZpZXdUcmFmb0ludjsNCn07DQoNCmxheW91dChzdGQxNDApDQp1bmlmb3JtIEdsb2JhbA0Kew0KICAgIGludCBQaWNrSWQ7DQp9Ow0KDQpsYXlvdXQoc3RkMTQwKQ0KdW5pZm9ybSBQZXJWaWV3DQp7DQogICAgbWF0NHg0IFZpZXdQcm9qVHJhZm87DQp9Ow0KDQp1bmlmb3JtIHNhbXBsZXIyRCBkaWZmdXNlU2FtcGxlcjsNCg0KDQojaWZkZWYgVmVydGV4DQoNCmxheW91dChsb2NhdGlvbiA9IDApIGluIHZlYzIgRGlmZnVzZUNvbG9yQ29vcmRpbmF0ZXM7DQpsYXlvdXQobG9jYXRpb24gPSAxKSBpbiB2ZWMzIE5vcm1hbHM7DQpsYXlvdXQobG9jYXRpb24gPSAyKSBpbiB2ZWM0IFBvc2l0aW9uczsNCm91dCB2ZWMyIGZzX0RpZmZ1c2VDb2xvckNvb3JkaW5hdGVzOw0Kb3V0IHZlYzMgZnNfVmlld1NwYWNlTm9ybWFsOw0Kdm9pZCBtYWluKCkNCnsNCiAgICBmc19EaWZmdXNlQ29sb3JDb29yZGluYXRlcyA9IERpZmZ1c2VDb2xvckNvb3JkaW5hdGVzOw0KICAgIGdsX1Bvc2l0aW9uID0gKChQb3NpdGlvbnMgKiBNb2RlbFRyYWZvKSAqIFZpZXdQcm9qVHJhZm8pOw0KICAgIGZzX1ZpZXdTcGFjZU5vcm1hbCA9IG5vcm1hbGl6ZSgoTW9kZWxWaWV3VHJhZm9JbnYgKiB2ZWM0KE5vcm1hbHMsIDAuMCkpLnh5eik7DQp9DQoNCiNlbmRpZg0KDQoNCg0KI2lmZGVmIEZyYWdtZW50DQoNCnZlYzIgQWFyZHZhcmtfRG9tX05vcm1hbDMyX3Nnbl9Db1ZsNTFPZF9JbFpFVmJZREkzNzhxYTlqcFkodmVjMiB2KQ0Kew0KICAgIHJldHVybiB2ZWMyKCgodi54ID49IDAuMCkgPyAxLjAgOiAtMS4wKSwgKCh2LnkgPj0gMC4wKSA/IDEuMCA6IC0xLjApKTsNCn0NCg0KDQp2ZWMyIEFhcmR2YXJrX0RvbV9Ob3JtYWwzMl9jbGFtcF9mMkZnbGJuR0ZhZ1J1TzFHbHNXMDBoZ0pfZUNjKHZlYzIgdikNCnsNCiAgICByZXR1cm4gdmVjMigoKHYueCA8IC0xLjApID8gLTEuMCA6ICgodi54ID4gMS4wKSA/IDEuMCA6IHYueCkpLCAoKHYueSA8IC0xLjApID8gLTEuMCA6ICgodi55ID4gMS4wKSA/IDEuMCA6IHYueSkpKTsNCn0NCg0KDQp2ZWMzIEFhcmR2YXJrX0RvbV9Ob3JtYWwzMl9kZWNvZGVfWGliQ3JNbzNiSHhiZW03bnFlV0dXZGdrMDBIYyhpbnQgdikNCnsNCiAgICBpZigodiA9PSAwKSkNCiAgICB7DQogICAgICAgIHJldHVybiB2ZWMzKDAuMCwgMC4wLCAwLjApOw0KICAgIH0NCiAgICBlbHNlDQogICAgew0KICAgICAgICB2ZWMyIGUgPSAoKHZlYzIoKGZsb2F0KCh1aW50KHYpID4+IDE2KSkgLyA2NTUzNS4wKSwgKGZsb2F0KCh2ICYgNjU1MzUpKSAvIDY1NTM1LjApKSAqIDIuMCkgLSB2ZWMyKDEuMCwgMS4wKSk7DQogICAgICAgIHZlYzMgdjEgPSB2ZWMzKGUsICgoMS4wIC0gYWJzKGUueCkpIC0gYWJzKGUueSkpKTsNCiAgICAgICAgaWYoKHYxLnogPCAwLjApKQ0KICAgICAgICB7DQogICAgICAgICAgICByZXR1cm4gbm9ybWFsaXplKHZlYzMoKHZlYzIoKDEuMCAtIGFicyh2MS55KSksICgxLjAgLSBhYnModjEueCkpKSAqIEFhcmR2YXJrX0RvbV9Ob3JtYWwzMl9zZ25fQ29WbDUxT2RfSWxaRVZiWURJMzc4cWE5anBZKHYxLnh5KSksIHYxLnopKTsNCiAgICAgICAgfQ0KICAgICAgICBlbHNlDQogICAgICAgIHsNCiAgICAgICAgICAgIHJldHVybiBub3JtYWxpemUodjEpOw0KICAgICAgICB9DQogICAgfQ0KfQ0KDQoNCmludCBBYXJkdmFya19Eb21fTm9ybWFsMzJfZW5jb2RlX2IwMHhJQkRCX2xvQ3BtN1ZEREhJanRMMDB6V29nKHZlYzMgdikNCnsNCiAgICBpZigoKCh2LnggPT0gMC4wKSAmJiAodi55ID09IDAuMCkpICYmICh2LnogPT0gMC4wKSkpDQogICAgew0KICAgICAgICByZXR1cm4gMDsNCiAgICB9DQogICAgZWxzZQ0KICAgIHsNCiAgICAgICAgdmVjMiBwID0gKHYueHkgKiAoMS4wIC8gKChhYnModi54KSArIGFicyh2LnkpKSArIGFicyh2LnopKSkpOw0KICAgICAgICB2ZWMyIHAxID0gKCh2LnogPD0gMC4wKSA/IEFhcmR2YXJrX0RvbV9Ob3JtYWwzMl9jbGFtcF9mMkZnbGJuR0ZhZ1J1TzFHbHNXMDBoZ0pfZUNjKCh2ZWMyKCgxLjAgLSBhYnMocC55KSksICgxLjAgLSBhYnMocC54KSkpICogQWFyZHZhcmtfRG9tX05vcm1hbDMyX3Nnbl9Db1ZsNTFPZF9JbFpFVmJZREkzNzhxYTlqcFkocCkpKSA6IEFhcmR2YXJrX0RvbV9Ob3JtYWwzMl9jbGFtcF9mMkZnbGJuR0ZhZ1J1TzFHbHNXMDBoZ0pfZUNjKHApKTsNCiAgICAgICAgZmxvYXQgYmVzdERvdCA9IDAuMDsNCiAgICAgICAgaW50IGJlc3QgPSAwOw0KICAgICAgICBmb3IoaW50IGR4ID0gMDsgKGR4IDwgMik7IGR4KyspDQogICAgICAgIHsNCiAgICAgICAgICAgIGZvcihpbnQgZHkgPSAwOyAoZHkgPCAyKTsgZHkrKykNCiAgICAgICAgICAgIHsNCiAgICAgICAgICAgICAgICBpbnQgZSA9ICgoKGludChmbG9vcigoKChwMS54ICogMC41KSArIDAuNSkgKiA2NTUzNS4wKSkpICsgZHgpIDw8IDE2KSB8IChpbnQoZmxvb3IoKCgocDEueSAqIDAuNSkgKyAwLjUpICogNjU1MzUuMCkpKSArIGR5KSk7DQogICAgICAgICAgICAgICAgZmxvYXQgZCA9IGRvdChBYXJkdmFya19Eb21fTm9ybWFsMzJfZGVjb2RlX1hpYkNyTW8zYkh4YmVtN25xZVdHV2RnazAwSGMoZSksIHYpOw0KICAgICAgICAgICAgICAgIGlmKChkID4gYmVzdERvdCkpDQogICAgICAgICAgICAgICAgew0KICAgICAgICAgICAgICAgICAgICBiZXN0RG90ID0gZDsNCiAgICAgICAgICAgICAgICAgICAgYmVzdCA9IGU7DQogICAgICAgICAgICAgICAgfQ0KICAgICAgICAgICAgfQ0KICAgICAgICB9DQogICAgICAgIHJldHVybiBiZXN0Ow0KICAgIH0NCn0NCg0KDQppbiB2ZWMyIGZzX0RpZmZ1c2VDb2xvckNvb3JkaW5hdGVzOw0KaW4gdmVjMyBmc19WaWV3U3BhY2VOb3JtYWw7DQpsYXlvdXQobG9jYXRpb24gPSAwKSBvdXQgdmVjNCBDb2xvcnNPdXQ7DQpsYXlvdXQobG9jYXRpb24gPSAxKSBvdXQgaXZlYzQgUGlja0lkT3V0Ow0Kdm9pZCBtYWluKCkNCnsNCiAgICBDb2xvcnNPdXQgPSB0ZXh0dXJlKGRpZmZ1c2VTYW1wbGVyLCBmc19EaWZmdXNlQ29sb3JDb29yZGluYXRlcyk7DQogICAgZ2xfRnJhZ0RlcHRoID0gZ2xfRnJhZ0Nvb3JkLno7DQogICAgUGlja0lkT3V0ID0gaXZlYzQoUGlja0lkLCBpbnQoQWFyZHZhcmtfRG9tX05vcm1hbDMyX2VuY29kZV9iMDB4SUJEQl9sb0NwbTdWRERISWp0TDAweldvZyhub3JtYWxpemUoZnNfVmlld1NwYWNlTm9ybWFsKSkpLCBmbG9hdEJpdHNUb0ludCgoKDIuMCAqIGdsX0ZyYWdDb29yZC56KSAtIDEuMCkpLCAwKTsNCn0NCg0KI2VuZGlmDQo="
            "xo/Bb/IvRVP0g6hhEv4Ya7DYNgk=xb2jMUM74e73GH/AjbzDD8s2FeU=_Colors:0", "CkdMU0xTaGFkZXIDAAAAAAAAAAZDb2xvcnMGQ29sb3JzBAQDIAEAAAAAAQAAAAdOb3JtYWxzB05vcm1hbHMEAwMgAQAAAAACAAAACVBvc2l0aW9ucwlQb3NpdGlvbnMEBAMgAQAAAAABAAAAAAAAAAlDb2xvcnNPdXQGQ29sb3JzBAQDIAEAAAAAAAAAAAAAAAAAAAAAAwAAAAhQZXJMaWdodP////8IUGVyTGlnaHT/////EAAAAAEAAAANTGlnaHRMb2NhdGlvbgAAAAAEAwMgCFBlck1vZGVs/////whQZXJNb2RlbP////+AAAAAAgAAAApNb2RlbFRyYWZvAAAAAAUEBAMgDU1vZGVsVHJhZm9JbnZAAAAABQQEAyAHUGVyVmlld/////8HUGVyVmlld/////9AAAAAAQAAAA1WaWV3UHJvalRyYWZvAAAAAAUEBAMgAAAAAAACAAAAAAAAAAAAAAAEbWFpbgMAAAAAAAAABkNvbG9ycwZDb2xvcnMEBAMgAQAAAAABAAAAB05vcm1hbHMHTm9ybWFscwQDAyABAAAAAAIAAAAJUG9zaXRpb25zCVBvc2l0aW9ucwQEAyABAAAAAAMAAAAAAAAACWZzX0NvbG9ycwZDb2xvcnMEBAMgAQAAAAABAAAACmZzX05vcm1hbHMHTm9ybWFscwQDAyABAAAAAAIAAAAQZnNfV29ybGRQb3NpdGlvbg1Xb3JsZFBvc2l0aW9uBAQDIAEAAAAAAAAAAAAAAAAAAAAAAgAAAAhQZXJNb2RlbAdQZXJWaWV3AAAAAAAAAAAAAAAAAQAAAAEAAAABAAAAC2dsX1Bvc2l0aW9uBAQDIAQAAAAEAAAABG1haW4DAAAAAAAAAAlmc19Db2xvcnMGQ29sb3JzBAQDIAEAAAAAAQAAAApmc19Ob3JtYWxzB05vcm1hbHMEAwMgAQAAAAACAAAAEGZzX1dvcmxkUG9zaXRpb24NV29ybGRQb3NpdGlvbgQEAyABAAAAAAEAAAAAAAAACUNvbG9yc091dAZDb2xvcnMEBAMgAQAAAAAAAAAAAAAAAAAAAAABAAAACFBlckxpZ2h0AAAAAAIAAAAJbm9ybWFsaXplBAMDIAEAAAAEAwMgA2FicwMgAQAAAAMgAAAAAAEAAAAAAAAAAQAAAAtnbF9Qb3NpdGlvbgQEAyD6ByN2ZXJzaW9uIDMwMCBlcw0KDQoNCg0KbGF5b3V0KHN0ZDE0MCkNCnVuaWZvcm0gUGVyTGlnaHQNCnsNCiAgICB2ZWMzIExpZ2h0TG9jYXRpb247DQp9Ow0KDQpsYXlvdXQoc3RkMTQwKQ0KdW5pZm9ybSBQZXJNb2RlbA0Kew0KICAgIG1hdDR4NCBNb2RlbFRyYWZvOw0KICAgIG1hdDR4NCBNb2RlbFRyYWZvSW52Ow0KfTsNCg0KbGF5b3V0KHN0ZDE0MCkNCnVuaWZvcm0gUGVyVmlldw0Kew0KICAgIG1hdDR4NCBWaWV3UHJvalRyYWZvOw0KfTsNCg0KDQojaWZkZWYgVmVydGV4DQoNCmxheW91dChsb2NhdGlvbiA9IDApIGluIHZlYzQgQ29sb3JzOw0KbGF5b3V0KGxvY2F0aW9uID0gMSkgaW4gdmVjMyBOb3JtYWxzOw0KbGF5b3V0KGxvY2F0aW9uID0gMikgaW4gdmVjNCBQb3NpdGlvbnM7DQpvdXQgdmVjNCBmc19Db2xvcnM7DQpvdXQgdmVjMyBmc19Ob3JtYWxzOw0Kb3V0IHZlYzQgZnNfV29ybGRQb3NpdGlvbjsNCnZvaWQgbWFpbigpDQp7DQogICAgdmVjNCB3cCA9IChQb3NpdGlvbnMgKiBNb2RlbFRyYWZvKTsNCiAgICBmc19Db2xvcnMgPSBDb2xvcnM7DQogICAgZnNfTm9ybWFscyA9IChNb2RlbFRyYWZvSW52ICogdmVjNChOb3JtYWxzLCAwLjApKS54eXo7DQogICAgZ2xfUG9zaXRpb24gPSAod3AgKiBWaWV3UHJvalRyYWZvKTsNCiAgICBmc19Xb3JsZFBvc2l0aW9uID0gd3A7DQp9DQoNCiNlbmRpZg0KDQoNCg0KI2lmZGVmIEZyYWdtZW50DQoNCmluIHZlYzQgZnNfQ29sb3JzOw0KaW4gdmVjMyBmc19Ob3JtYWxzOw0KaW4gdmVjNCBmc19Xb3JsZFBvc2l0aW9uOw0KbGF5b3V0KGxvY2F0aW9uID0gMCkgb3V0IHZlYzQgQ29sb3JzT3V0Ow0Kdm9pZCBtYWluKCkNCnsNCiAgICBmbG9hdCBkaWZmdXNlID0gYWJzKGRvdChub3JtYWxpemUoKExpZ2h0TG9jYXRpb24gLSBmc19Xb3JsZFBvc2l0aW9uLnh5eikpLCBub3JtYWxpemUoZnNfTm9ybWFscykpKTsNCiAgICBDb2xvcnNPdXQgPSB2ZWM0KChmc19Db2xvcnMueHl6ICogZGlmZnVzZSksIGZzX0NvbG9ycy53KTsNCn0NCg0KI2VuZGlmDQo="
            "y/2cE70wJv2V76bV7/2YA6lggMY=dPN8Vh1RgW3Qv1NcEoySoaqjaUw=jyBPwMc+GTr5gKev24IlgoiCx4E=_Colors:0", "CkdMU0xTaGFkZXIFAAAAAAAAAAdLTE1LaW5kB0tMTUtpbmQEBAMgAQgAAAABAAAACVBhdGhDb2xvcglQYXRoQ29sb3IEBAMgAQAAAAACAAAACVBvc2l0aW9ucwlQb3NpdGlvbnMEBAMgAQAAAAADAAAADFNoYXBlVHJhZm9SMAxTaGFwZVRyYWZvUjAEBAMgAQAAAAAEAAAADFNoYXBlVHJhZm9SMQxTaGFwZVRyYWZvUjEEBAMgAQAAAAABAAAAAAAAAAlDb2xvcnNPdXQGQ29sb3JzBAQDIAEAAAAAAAAAAAAAAAAAAAAAAwAAAAZHbG9iYWz/////Bkdsb2JhbP////8QAAAAAgAAAAlEZXB0aEJpYXMAAAAAAyAKRmlsbEdseXBocwQAAAAACFBlck1vZGVs/////whQZXJNb2RlbP////9AAAAAAQAAAA5Nb2RlbFZpZXdUcmFmbwAAAAAFBAQDIAdQZXJWaWV3/////wdQZXJWaWV3/////0AAAAABAAAACVByb2pUcmFmbwAAAAAFBAQDIAAAAAAAAgAAAAAAAAAAAAAABG1haW4FAAAAAAAAAAdLTE1LaW5kB0tMTUtpbmQEBAMgAQgAAAABAAAACVBhdGhDb2xvcglQYXRoQ29sb3IEBAMgAQAAAAACAAAACVBvc2l0aW9ucwlQb3NpdGlvbnMEBAMgAQAAAAADAAAADFNoYXBlVHJhZm9SMAxTaGFwZVRyYWZvUjAEBAMgAQAAAAAEAAAADFNoYXBlVHJhZm9SMQxTaGFwZVRyYWZvUjEEBAMgAQAAAAACAAAAAAAAAApmc19LTE1LaW5kB0tMTUtpbmQEBAMgAQgAAAABAAAADGZzX1BhdGhDb2xvcglQYXRoQ29sb3IEBAMgAQAAAAAAAAAAAAAAAAAAAAADAAAABkdsb2JhbAhQZXJNb2RlbAdQZXJWaWV3AAAAAAEAAAADYWJzAyABAAAAAyAAAAAAAQAAAAEAAAABAAAAC2dsX1Bvc2l0aW9uBAQDIAQAAAAEAAAABG1haW4CAAAAAAAAAApmc19LTE1LaW5kB0tMTUtpbmQEBAMgAQgAAAABAAAADGZzX1BhdGhDb2xvcglQYXRoQ29sb3IEBAMgAQAAAAABAAAAAAAAAAlDb2xvcnNPdXQGQ29sb3JzBAQDIAEAAAAAAAAAAAAAAAAAAAAAAQAAAAZHbG9iYWwAAAAAAQAAAAdkaXNjYXJkAQAAAAAAAAAAAQAAAAAAAAACAAAAC2dsX1Bvc2l0aW9uBAQDIBFnbF9TYW1wbGVQb3NpdGlvbgQCAyCSGSN2ZXJzaW9uIDMwMCBlcw0KDQoNCg0KbGF5b3V0KHN0ZDE0MCkNCnVuaWZvcm0gR2xvYmFsDQp7DQogICAgZmxvYXQgRGVwdGhCaWFzOw0KICAgIGJvb2wgRmlsbEdseXBoczsNCn07DQoNCmxheW91dChzdGQxNDApDQp1bmlmb3JtIFBlck1vZGVsDQp7DQogICAgbWF0NHg0IE1vZGVsVmlld1RyYWZvOw0KfTsNCg0KbGF5b3V0KHN0ZDE0MCkNCnVuaWZvcm0gUGVyVmlldw0Kew0KICAgIG1hdDR4NCBQcm9qVHJhZm87DQp9Ow0KDQoNCiNpZmRlZiBWZXJ0ZXgNCg0KbGF5b3V0KGxvY2F0aW9uID0gMCkgaW4gdmVjNCBLTE1LaW5kOw0KbGF5b3V0KGxvY2F0aW9uID0gMSkgaW4gdmVjNCBQYXRoQ29sb3I7DQpsYXlvdXQobG9jYXRpb24gPSAyKSBpbiB2ZWM0IFBvc2l0aW9uczsNCmxheW91dChsb2NhdGlvbiA9IDMpIGluIHZlYzQgU2hhcGVUcmFmb1IwOw0KbGF5b3V0KGxvY2F0aW9uID0gNCkgaW4gdmVjNCBTaGFwZVRyYWZvUjE7DQpzYW1wbGUgb3V0IHZlYzQgZnNfS0xNS2luZDsNCm91dCB2ZWM0IGZzX1BhdGhDb2xvcjsNCnZvaWQgbWFpbigpDQp7DQogICAgdmVjNCBwID0gdmVjNCgwLjAsIDAuMCwgMC4wLCAwLjApOw0KICAgIHZlYzIgcG0gPSB2ZWMyKGRvdChTaGFwZVRyYWZvUjAueHl6LCB2ZWMzKFBvc2l0aW9ucy54eSwgMS4wKSksIGRvdChTaGFwZVRyYWZvUjEueHl6LCB2ZWMzKFBvc2l0aW9ucy54eSwgMS4wKSkpOw0KICAgIGlmKChTaGFwZVRyYWZvUjAudyA8IDAuMCkpDQogICAgew0KICAgICAgICBpZigoKCgoYWJzKFByb2pUcmFmb1szXVswXSkgPCAxRS0wNSkgJiYgKGFicyhQcm9qVHJhZm9bM11bMV0pIDwgMUUtMDUpKSAmJiAoYWJzKFByb2pUcmFmb1szXVsyXSkgPCAxRS0wNSkpID8gKE1vZGVsVmlld1RyYWZvWzBdWzBdID4gMC4wKSA6IChkb3QodmVjMyhNb2RlbFZpZXdUcmFmb1swXVszXSwgTW9kZWxWaWV3VHJhZm9bMV1bM10sIE1vZGVsVmlld1RyYWZvWzJdWzNdKSwgdmVjMyhNb2RlbFZpZXdUcmFmb1swXVsyXSwgTW9kZWxWaWV3VHJhZm9bMV1bMl0sIE1vZGVsVmlld1RyYWZvWzJdWzJdKSkgPCAwLjApKSkNCiAgICAgICAgew0KICAgICAgICAgICAgcCA9ICh2ZWM0KHBtLngsIHBtLnksIFBvc2l0aW9ucy56LCBQb3NpdGlvbnMudykgKiBNb2RlbFZpZXdUcmFmbyk7DQogICAgICAgIH0NCiAgICAgICAgZWxzZQ0KICAgICAgICB7DQogICAgICAgICAgICBwID0gKHZlYzQoKC1wbS54KSwgcG0ueSwgUG9zaXRpb25zLnosIFBvc2l0aW9ucy53KSAqIE1vZGVsVmlld1RyYWZvKTsNCiAgICAgICAgfQ0KICAgIH0NCiAgICBlbHNlDQogICAgew0KICAgICAgICBwID0gKHZlYzQocG0ueCwgcG0ueSwgUG9zaXRpb25zLnosIFBvc2l0aW9ucy53KSAqIE1vZGVsVmlld1RyYWZvKTsNCiAgICB9DQogICAgdmVjNCBQb3NpdGlvbnNDID0gKHAgKiBQcm9qVHJhZm8pOw0KICAgIGZzX0tMTUtpbmQgPSBLTE1LaW5kOw0KICAgIGZzX1BhdGhDb2xvciA9IFBhdGhDb2xvcjsNCiAgICBnbF9Qb3NpdGlvbiA9IChQb3NpdGlvbnNDIC0gdmVjNCgwLjAsIDAuMCwgKDAuMCAqIERlcHRoQmlhcyksIDAuMCkpOw0KfQ0KDQojZW5kaWYNCg0KDQoNCiNpZmRlZiBGcmFnbWVudA0KDQpzYW1wbGUgaW4gdmVjNCBmc19LTE1LaW5kOw0KaW4gdmVjNCBmc19QYXRoQ29sb3I7DQpsYXlvdXQobG9jYXRpb24gPSAwKSBvdXQgdmVjNCBDb2xvcnNPdXQ7DQp2b2lkIG1haW4oKQ0Kew0KICAgIGZsb2F0IGtpbmQgPSAoZnNfS0xNS2luZC53ICsgKDAuMDAxICogZ2xfU2FtcGxlUG9zaXRpb24ueCkpOw0KICAgIHZlYzQgY29sb3IgPSBmc19QYXRoQ29sb3I7DQogICAgaWYoRmlsbEdseXBocykNCiAgICB7DQogICAgICAgIGlmKCgoa2luZCA+IDEuNSkgJiYgKGtpbmQgPCAzLjUpKSkNCiAgICAgICAgew0KICAgICAgICAgICAgaWYoKCgoKGZzX0tMTUtpbmQueHl6LnggKiBmc19LTE1LaW5kLnh5ei54KSAtIGZzX0tMTUtpbmQueHl6LnkpICogZnNfS0xNS2luZC54eXoueikgPiAwLjApKQ0KICAgICAgICAgICAgew0KICAgICAgICAgICAgICAgIGRpc2NhcmQ7DQogICAgICAgICAgICB9DQogICAgICAgIH0NCiAgICAgICAgZWxzZQ0KICAgICAgICB7DQogICAgICAgICAgICBpZigoKGtpbmQgPiAzLjUpICYmIChraW5kIDwgNS41KSkpDQogICAgICAgICAgICB7DQogICAgICAgICAgICAgICAgaWYoKCgoKChmc19LTE1LaW5kLnh5ei54ICogZnNfS0xNS2luZC54eXoueCkgKyAoZnNfS0xNS2luZC54eXoueSAqIGZzX0tMTUtpbmQueHl6LnkpKSAtIDEuMCkgKiBmc19LTE1LaW5kLnh5ei56KSA+IDAuMCkpDQogICAgICAgICAgICAgICAgew0KICAgICAgICAgICAgICAgICAgICBkaXNjYXJkOw0KICAgICAgICAgICAgICAgIH0NCiAgICAgICAgICAgIH0NCiAgICAgICAgICAgIGVsc2UNCiAgICAgICAgICAgIHsNCiAgICAgICAgICAgICAgICBpZigoa2luZCA+IDUuNSkpDQogICAgICAgICAgICAgICAgew0KICAgICAgICAgICAgICAgICAgICBpZigoKCgoZnNfS0xNS2luZC54eXoueCAqIGZzX0tMTUtpbmQueHl6LngpICogZnNfS0xNS2luZC54eXoueCkgLSAoZnNfS0xNS2luZC54eXoueSAqIGZzX0tMTUtpbmQueHl6LnopKSA+IDAuMCkpDQogICAgICAgICAgICAgICAgICAgIHsNCiAgICAgICAgICAgICAgICAgICAgICAgIGRpc2NhcmQ7DQogICAgICAgICAgICAgICAgICAgIH0NCiAgICAgICAgICAgICAgICB9DQogICAgICAgICAgICB9DQogICAgICAgIH0NCiAgICB9DQogICAgZWxzZQ0KICAgIHsNCiAgICAgICAgaWYoKChraW5kID4gMS41KSAmJiAoa2luZCA8IDMuNSkpKQ0KICAgICAgICB7DQogICAgICAgICAgICBjb2xvciA9IHZlYzQoMS4wLCAwLjAsIDAuMCwgMS4wKTsNCiAgICAgICAgfQ0KICAgICAgICBlbHNlDQogICAgICAgIHsNCiAgICAgICAgICAgIGlmKCgoa2luZCA+IDMuNSkgJiYgKGtpbmQgPCA1LjUpKSkNCiAgICAgICAgICAgIHsNCiAgICAgICAgICAgICAgICBjb2xvciA9IHZlYzQoMC4wLCAxLjAsIDAuMCwgMS4wKTsNCiAgICAgICAgICAgIH0NCiAgICAgICAgICAgIGVsc2UNCiAgICAgICAgICAgIHsNCiAgICAgICAgICAgICAgICBpZigoa2luZCA+IDUuNSkpDQogICAgICAgICAgICAgICAgew0KICAgICAgICAgICAgICAgICAgICBjb2xvciA9IHZlYzQoMC4wLCAwLjAsIDEuMCwgMS4wKTsNCiAgICAgICAgICAgICAgICB9DQogICAgICAgICAgICB9DQogICAgICAgIH0NCiAgICB9DQogICAgQ29sb3JzT3V0ID0gY29sb3I7DQp9DQoNCiNlbmRpZg0K"
        |]
        


    let run gl (ui : Env<unit> -> DomNode) =

        match LocalStorage.TryGet "uishadercache" with
        | Some v when v = "1" -> ()
        | _ ->
            for (k, v) in cacheContent do
                LocalStorage.Set(k, v)
            LocalStorage.Set("uishadercache", "1")
        

        let app =
            {
                initial = ()
                update = fun _ () () -> ()
                view = fun e () -> ui e
                unpersist =
                    {
                        init = fun () -> ()
                        update = fun () () -> ()
                    }
            }
        Boot.run gl app

[<AutoOpen>]
module RenderingHelpers =
        
    type RenderBuilder() =
        inherit RenderControlBuilder()
        
        member x.Run(run : NodeBuilderHelpers.RenderControlBuilder<unit>) =
            let getContent (info : RenderControlInfo) =
                
                let time =
                    let t0 = DateTime.Now
                    let sw = System.Diagnostics.Stopwatch.StartNew()
                    AVal.custom (fun _ ->
                        t0 + sw.Elapsed
                    )
                let state = NodeBuilderHelpers.RenderControlBuilderState( { info with Time = time } )
                
                let mutable camera = OrbitState.create V3d.Zero 1.0 0.4 7.0 Button.Left Button.Right
                let cam = cval camera.view
                let coll = new AsyncCollection<seq<OrbitMessage>>()
                let orbitEnv =
                    { new Env<OrbitMessage>  with
                        member x.Emit messages =
                            coll.Put messages
                        member x.Run(js, cb) =
                            printfn "cannot run JS"
                        member x.RunModal _ =
                            printfn "cannot run modal"
                            { new IDisposable with member x.Dispose() = () }
                        member x.Runtime =
                            info.Runtime
                    }
                  
                let runner =
                    task {
                        while true do
                            let! msgs = coll.Take()
                            for msg in msgs do
                                camera <- OrbitController.update orbitEnv camera msg
                                
                                match msg with
                                | OrbitMessage.Rendered -> 
                                    transact (fun () -> time.MarkOutdated())
                                | _ ->
                                    ()
                            //camera <- OrbitState.withView camera
                            transact (fun () -> cam.Value <- camera.view)
                    }
                    
                let camera, attributes =
                    let att = OrbitController.getAttributes orbitEnv
                    cam, att
                    
                
                let a, b =
                    RenderControl.OnRendered (fun _ -> 
                        orbitEnv.Emit [OrbitMessage.Rendered]
                    )
                state.Append (a, b)
                
                state.Append attributes
                state.Append [
                    Sg.View (
                        camera |> AVal.map (fun v ->
                            CameraView.viewTrafo v
                        )
                    )
                    
                    Sg.Proj (
                        info.ViewportSize |> AVal.map (fun s ->
                            Frustum.perspective 50.0 0.1 100.0 (float s.X / float s.Y)
                            |> Frustum.projTrafo
                        ) 
                    )
                    
                    Sg.OnDoubleTap (fun e ->
                        orbitEnv.Emit [ OrbitMessage.SetTargetCenter(true, AnimationKind.Tanh, e.WorldPosition) ]    
                    )
                    
                ]
                run state
                state.Build()
            DomNode.RenderControl(getContent)
        
    let simpleRenderControl = RenderBuilder()
      
