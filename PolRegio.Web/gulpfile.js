/// <binding AfterBuild='builddev' />
var $ = require('gulp-load-plugins')();
var browser = require('browser-sync');
var gulp = require('gulp');
var sequence = require('run-sequence');
var sourcemaps = require('gulp-sourcemaps');
var yargs = require('yargs'); 

var COMPATIBILITY = ['last 3 versions', 'ie >= 9', 'android >= 2.3.3', 'iOS >= 6'];
const PRODUCTION = !!(yargs.argv.production); 

var PATHS = {
    sass: [
        'bower_components/notosans-fontface/scss/',
        'bower_components/bootstrap-sass/assets/stylesheets/',
        'bower_components/eonasdan-bootstrap-datetimepicker/src/sass/',
        'bower_components/font-awesome/scss/',
        'bower_components/bootstrap-select/sass/',
        'bower_components/bxslider-4/'
    ],
    fonts: [
        'bower_components/notosans-fontface/fonts/**/*.*',
        'bower_components/font-awesome/fonts/**/*.*',
        'bower_components/bootstrap-sass/assets/fonts/**/*.*'
    ],
    javascript: [
        'bower_components/jquery/dist/jquery.js',
		'bower_components/jquery.cookie/jquery.cookie.js',
        'bower_components/mobile-detect/mobile-detect.min.js',
        'bower_components/svg-pan-zoom/dist/svg-pan-zoom.js',
		'bower_components/headroom.js/index.js',
        'bower_components/jQuery.headroom.js/index.js',
		'bower_components/bootstrap-sass/assets/javascripts/bootstrap/affix.js',
		'bower_components/bootstrap-sass/assets/javascripts/bootstrap/tooltip.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/alert.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/button.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/carousel.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/collapse.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/dropdown.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/modal.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/popover.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/scrollspy.js',
        'bower_components/bootstrap-sass/assets/javascripts/bootstrap/tab.js',
		'bower_components/bootstrap-sass/assets/javascripts/bootstrap/transition.js',
        'bower_components/moment/min/moment-with-locales.min.js',
        'bower_components/eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js',
		'bower_components/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.min.js',
        'bower_components/devbridge-autocomplete/dist/jquery.autocomplete.min.js',
        'bower_components/bootstrap-select/js/bootstrap-select.js',
        'bower_components/bxslider-4/jquery.bxslider.min.js',
        'bower_components/jquery-validation/dist/jquery.validate.js',
        'bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js',
        'bower_components/listgroup.js/listgroup.js',

        './scripts/polreg.js'
    ]
};

gulp.task('sass', function() {
    return gulp.src('css/scss/app.scss')
        .pipe(sourcemaps.init())
        .pipe($.sass({
                includePaths: PATHS.sass,
                outputStyle: 'compressed'
            })
            .on('error', $.sass.logError))
        .pipe($.autoprefixer({
            browsers: COMPATIBILITY
        }))
        .pipe($.if(!PRODUCTION, $.sourcemaps.write())) 
        .pipe(gulp.dest('./css'))
        .pipe(browser.reload({
            stream: true
        }));
});

gulp.task('javascript', function() {
    return [
        gulp.src(PATHS.javascript)
        .pipe($.concat('polreg.min.js'))
        .pipe($.uglify())
        .pipe(gulp.dest('./scripts')),
        gulp.src("./scripts/markercluster.js")
        .pipe($.concat('markercluster.min.js'))
        .pipe($.uglify())
        .pipe(gulp.dest('./scripts'))
    ];
});

gulp.task('javascriptdev', function () {
    return [
        gulp.src(PATHS.javascript)
        .pipe($.concat('polreg.min.js'))
        .pipe(gulp.dest('./scripts')),

        gulp.src("./scripts/markercluster.js")
        .pipe($.concat('markercluster.min.js'))
        .pipe(gulp.dest('./scripts'))
    ];
});

gulp.task('fonts', function () {
    return gulp.src(PATHS.fonts)
        .pipe(gulp.dest('./fonts'));
});

gulp.task('build', function(done) {
    sequence('sass', ['javascript'], ['fonts'], done);
});

gulp.task('builddev', function(done) {
    sequence('sass',['javascriptdev'],['fonts'], done);
});


gulp.task('reload', function(cb) {
    browser.reload();
    cb();
});

gulp.task('server', ['builddev'], function() {
    browser.init({
        proxy: 'http://localhost:1200/',
        port: 8000,
        reloadDelay: 1000
    });
});

gulp.task('default', ['builddev', 'server'], function() {
    gulp.watch(['css/scss/**/*.scss'], ['sass']);
    gulp.watch(['./scripts/polreg.js'], ['javascriptdev', 'reload']);
    gulp.watch(['Views/**/*.cshtml'], ['reload']);
});